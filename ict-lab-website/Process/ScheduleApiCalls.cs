using ict_lab_website.Models.Schedule;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Process
{
    public class ScheduleApiCalls
    {
        private ApiCalls ApiCalls = new ApiCalls();
        private ApiConfig ApiConfig;
        
        public ScheduleApiCalls(ApiConfig apiConfig)
        {
            this.ApiConfig = apiConfig;
        }


        public Dictionary<int, Dictionary<int, Reservation>> GetReservationsForWeek(string roomName, int year, int quarter, int week)
        {
            string parameters = $"/{roomName}/{year}/4/22";
            Dictionary<int, Dictionary<int, Reservation>> reservationsForWeek = new Dictionary<int, Dictionary<int, Reservation>>();

            try
            {
                var json = ApiCalls.GetRequest(ApiConfig.Url + ApiConfig.GetWeek + parameters);
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
            catch(Exception e)
            {
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
