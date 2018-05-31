using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public interface ISchedule
    {
        Dictionary<int, Dictionary<int, Reservation>> GetWeek(DateTime dateTime, string roomName);
        Dictionary<int, Reservation> GetDay(DateTime dateTime, string roomName);        
        void AddReservation(Reservation reservation);
        List<DateTime> GetDatesInSameWeek(DateTime date);
        int GetNumberOfFreeTimeslots(DateTime date, string roomName);
    }
}
