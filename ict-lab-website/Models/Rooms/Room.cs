using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    // NOTE: This class will be completely changed once the schedule-functionality of the API becomes available.
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<FakeReservation> Reservations { get; set; } = new List<FakeReservation>();

        public List<FakeReservation> GetReservationsFor(ScheduleView scheduleView, DateTime dateTime)
        {
            switch (scheduleView)
            {
                case ScheduleView.Day:
                    return GetReservationsForDay(dateTime);
                case ScheduleView.Week:
                    return GetReservationsForWeek(dateTime);
                case ScheduleView.Month:
                    return GetReservationsForMonth(dateTime);
                case ScheduleView.Year:
                    return GetReservationsForYear(dateTime);
            }
            return null;
        }

        private List<FakeReservation> GetReservationsForMonth(DateTime dateTime)
        {
            var month = dateTime.Month;
            var year = dateTime.Year;
            var reservationsForMonth = Reservations.Where(x => x.DateAndTime.Month == month && x.DateAndTime.Year == year);
            return reservationsForMonth.ToList<FakeReservation>();
        }

        private List<FakeReservation> GetReservationsForDay(DateTime dateTime)
        {
            var reservationsForDay = Reservations.Where(x => x.DateAndTime.Date == dateTime.Date );
            return reservationsForDay.ToList<FakeReservation>();
        }

        private List<FakeReservation> GetReservationsForYear(DateTime dateTime)
        {
            var year = dateTime.Year;
            var reservationsForYear = Reservations.Where(x => x.DateAndTime.Year == year);
            return reservationsForYear.ToList<FakeReservation>();
        }

        private List<FakeReservation> GetReservationsForWeek(DateTime dateTime)
        {
            var week = GetIso8601WeekOfYear(dateTime);
            var reservationsForYear = Reservations.Where(x => GetIso8601WeekOfYear(x.DateAndTime) == week);
            return reservationsForYear.ToList<FakeReservation>();
        }

        //This method returns the weeknumber according to the ISO-8601 standard, because the one from .Net does strange things with weeks at the end of the year.
        //This method was found on: 
        //https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        private int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
