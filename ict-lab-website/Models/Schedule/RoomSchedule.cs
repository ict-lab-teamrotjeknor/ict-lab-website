using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class RoomSchedule : ISchedule
    {
        private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;
        private readonly ILogger logger;

        public RoomSchedule(IOptions<ApiConfig> apiConfig, ILogger<ScheduleController> logger)
        {
            apiCalls = new ApiCalls();
            this.apiConfig = apiConfig.Value;
            this.logger = logger;
        }

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

            return GetWeekFromApi(roomName, year, quarter, week);

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

        public void AddReservation(UploadableReservation reservation)
        {
            var reservationJsonObject = (JObject)JToken.FromObject(reservation);

            try
            {
                apiCalls.PostRequest(reservationJsonObject, apiConfig.Url + apiConfig.UploadHour);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Uploading reservation failed", DateTime.Now);
            }
        }


        public int GetNumberOfFreeTimeslots(DateTime date, string roomName)
        {
            var reservations = GetDay(date, roomName);
            return reservations.Where(x => x.Value != null).Count();
        }

        private Dictionary<int, Dictionary<int, Reservation>> GetWeekFromApi(string roomName, int year, int quarter, int week)
        {
            //The hardcoded 4 should be removed, but it is currently impossible to know what quarter is required by the API. 
            string parameters = $"/{roomName}/{year}/4/{week}";
            Dictionary<int, Dictionary<int, Reservation>> reservationsForWeek = new Dictionary<int, Dictionary<int, Reservation>>();

            try
            {
                logger.LogInformation("Getting week {roomName}, {year}, {quarter}, {week}  from API", roomName, year, quarter, week, DateTime.Now);

                var json = apiCalls.GetRequest(apiConfig.Url + apiConfig.GetWeek + parameters);
                var days = JObject.Parse(json)["Days"];
                int dayNumber = 1;

                foreach (var day in days)
                {
                    reservationsForWeek.Add(dayNumber, new Dictionary<int, Reservation>());
                    var hours = JObject.Parse(day.ToString())["Hours"];

                    foreach (var hour in hours)
                    {
                        Reservation reservation = hour.ToObject<Reservation>();
                        reservation.RoomId = roomName;
                        reservationsForWeek[dayNumber].Add(reservation.HourId, reservation);

                    }

                    for (int i = 1; i <= 15; i++)
                    {
                        if (!reservationsForWeek[dayNumber].ContainsKey(i))
                        {
                            reservationsForWeek[dayNumber].Add(i, null);
                        }
                    }
                    dayNumber++;
                }
                reservationsForWeek.Add(0, GetEmptyDay());
                reservationsForWeek.Add(6, GetEmptyDay());
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetWeek({roomName}, {year}, {quarter}, {week} NOT FOUND)", roomName, year, quarter, week, DateTime.Now);
                logger.LogInformation("Returning empty week", DateTime.Now);

                for (int i = 0; i < 7; i++)
                {
                    reservationsForWeek.Add(i, GetEmptyDay());
                }
            }
            return reservationsForWeek;
        }

        private Dictionary<int, Reservation> GetEmptyDay()
        {
            Dictionary<int, Reservation> emptyday = new Dictionary<int, Reservation>();

            for (int i = 1; i <= 15; i++)
            {
                emptyday.Add(i, null);
            }

            return emptyday;
        }
    }
}

