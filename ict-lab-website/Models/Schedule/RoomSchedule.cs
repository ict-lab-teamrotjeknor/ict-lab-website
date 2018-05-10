using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class RoomSchedule
    {
        // A dictionary for the schedule, which can be accessed in the form reservations[year][month][day][lessonhour].
        //For example: Reservations[2018][05][01][01] returns the reservation on the first lesson-hour on 01-05-2018.  
        public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Reservation>>>> Reservations { get; set; }

        public RoomSchedule()
        {
            Reservations = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Reservation>>>>();
            for (int year = 2016; year <= 2020; year++)
            {
                Reservations.Add(year, new Dictionary<int, Dictionary<int, Dictionary<int, Reservation>>>());
                for (int month = 1; month <= 12; month++)
                {
                    Reservations[year].Add(month, new Dictionary<int, Dictionary<int, Reservation>>());

                    int numberOfDays = DateTime.DaysInMonth(year, month);
                    for (int day = 1; day <= numberOfDays; day++)
                    {
                        Reservations[year][month].Add(day, new Dictionary<int, Reservation>());
                        int numberOfLessonHours = 15;
                        for (int hour = 1; hour <= numberOfLessonHours; hour++)
                        {
                            //Data for testing purposes:
                            if (hour == 1)
                            {
                                Reservations[year][month][day].Add(hour, new Reservation { Reserver = "MINUTO", StartLessonHour = 1, EndLessonHour = 1, RoomName = "H.1.308", Subject="ICT-LAB" });
                            }
                            else
                            {
                                Reservations[year][month][day].Add(hour, null);
                            }                          
                        }
                    }
                }
            }
        }

        public Dictionary<int, Dictionary<int, Dictionary<int, Reservation>>> GetReservationsForYear(DateTime date)
        {
            return Reservations[date.Year];
        }

        public Dictionary<int, Dictionary<int, Reservation>> GetReservationsForMonth(DateTime date)
        {
            return Reservations[date.Year][date.Month];
        }


        public Dictionary<int, Reservation> GetReservationsForDay(DateTime date)
        {
            return Reservations[date.Year][date.Month][date.Day];
        }

        public Dictionary<int, Dictionary<int, Reservation>> GetReservationsForWeek(DateTime date)
        {
            Dictionary<int, Dictionary<int, Reservation>> reservationsForWeek = new Dictionary<int, Dictionary<int, Reservation>>();
            int weeknumber = GetIso8601WeekOfYear(date);
            var ReservationsForYear = GetReservationsForYear(date);

            foreach(int monthKey in ReservationsForYear.Keys)
            {
                var reservationsForMonth = ReservationsForYear[monthKey];
                foreach(int dayKey in reservationsForMonth.Keys)
                {
                    var reservationsForDay = reservationsForMonth[dayKey];

                    DateTime day = new DateTime(date.Year, monthKey, dayKey);
                    int currentWeekNumber = GetIso8601WeekOfYear(day);
                    if (currentWeekNumber == weeknumber)
                    {
                        reservationsForWeek.Add(dayKey, reservationsForDay);
                    }
                }
            }
            return reservationsForWeek;
        }

        public List<DateTime> GetDatesForWeek(DateTime date)
        {
            List<DateTime> datesForWeek = new List<DateTime>();
            int weeknumber = GetIso8601WeekOfYear(date);
            var ReservationsForYear = GetReservationsForYear(date);

            foreach (int monthKey in ReservationsForYear.Keys)
            {
                var reservationsForMonth = ReservationsForYear[monthKey];
                foreach (int dayKey in reservationsForMonth.Keys)
                {
                    var reservationsForDay = reservationsForMonth[dayKey];

                    DateTime day = new DateTime(date.Year, monthKey, dayKey);
                    int currentWeekNumber = GetIso8601WeekOfYear(day);
                    if (currentWeekNumber == weeknumber)
                    {
                        datesForWeek.Add(day);
                    }
                }
            }
            return datesForWeek;
        }

        //This method returns the weeknumber according to the ISO-8601 standard, because the one from .Net does strange things with weeks at the end of the year.
        //This method was found on: 
        //https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        private int GetIso8601WeekOfYear(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public int getNumberOfFreeTimeSlots(DateTime date)
        {
            var reservations = this.GetReservationsForDay(date);
            return reservations.Where(x => x.Value != null).Count();
        }

        public void AddReservation(Reservation reservation)
        {
            var date = reservation.Date;
            var reservationsForDate = GetReservationsForDay(date);
            Boolean isAvailable = AreHoursAvailable(reservation, reservationsForDate);

            if (isAvailable)
            {
                for (int i = reservation.StartLessonHour; i <= reservation.EndLessonHour; i++)
                {
                    reservationsForDate[i] = reservation;
                }
            }
        }

        public Boolean AreHoursAvailable(Reservation reservation, Dictionary<int, Reservation> reservationsForDay)
        {
            Boolean areHoursAvailable = true;
            for (int i = reservation.StartLessonHour; i <= reservation.EndLessonHour; i++)
            {
                if (reservationsForDay[i] != null)
                {
                    areHoursAvailable = false;
                    break;
                }
            }
            return areHoursAvailable;
        }
    }
}
