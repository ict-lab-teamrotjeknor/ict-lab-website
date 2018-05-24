﻿using ict_lab_website.Models.Schedule;
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
        private ApiCalls apiCalls = new ApiCalls();
        private string url = "http://145.24.222.103:8080";


        public Dictionary<int, Dictionary<int, Reservation>> GetReservationsForWeek(string roomName, int year, int quarter, int week)
        {
            string parameters = $"/schedule/getweek/{roomName}/{year}/4/22";
            Dictionary<int, Dictionary<int, Reservation>> reservationsForWeek = new Dictionary<int, Dictionary<int, Reservation>>();

            try
            {
                var json = apiCalls.GetRequest(url + parameters);
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

                return reservationsForWeek;
            } 
            catch(Exception e)
            {
                throw e;
            }
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
