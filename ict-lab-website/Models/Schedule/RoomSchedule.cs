using ict_lab_website.Process;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class RoomSchedule : ISchedule
    {
        private ScheduleApiCalls scheduleAPiCalls = new ScheduleApiCalls();

        public Dictionary<int, Reservation> GetDay(DateTime date, string roomName)
        {
            var reservationsForWeek = GetWeek(date, roomName);
            int dayOfWeek = (int)date.DayOfWeek;
            Dictionary<int, Reservation> reservationsForDay = reservationsForWeek[dayOfWeek];

            return reservationsForDay.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<int, Dictionary<int, Reservation>> GetWeek(DateTime date, string roomName)
        {
            Dictionary<int, Dictionary<int, Reservation>> reservationsForWeek = new Dictionary<int, Dictionary<int, Reservation>>();
            int year = date.Year;
            int quarter = 4;
            int week = GetWeeknumber(date);

            return scheduleAPiCalls.GetReservationsForWeek(roomName, year, quarter, week);

        }

        public List<DateTime> GetDatesInSameWeek(DateTime date)
        {
            List<DateTime> datesInSameWeek = new List<DateTime>();
            var day = (int) date.DayOfWeek;
            int daysInWeek = 7;

            var monday = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
            datesInSameWeek.Add(monday);

            for (int i = (int)DayOfWeek.Monday; i < daysInWeek; i++)
            {
                var dateOfDay = new DateTime(monday.AddDays(i).Year, monday.AddDays(i).Month, monday.AddDays(i).Day);
                datesInSameWeek.Add(dateOfDay);
            }
            return datesInSameWeek;
        }

        //This method returns the weeknumber according to the ISO-8601 standard, because the one from .Net does strange things with weeks at the end of the year.
        //This method was found on: 
        //https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        private static int GetWeeknumber(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public void AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }


        public int GetNumberOfFreeTimeslots(DateTime date, string roomName)
        {
            var reservations = GetDay(date, roomName);
            return reservations.Where(x => x.Value != null).Count();
        }
    }
}
