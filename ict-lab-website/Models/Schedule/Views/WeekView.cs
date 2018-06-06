using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule.Views
{
    public class WeekView : IView
    {
        public string Name => "ScheduleViewWeek";

        public DateTime DecreaseDate(DateTime dateTime)
        {
            return dateTime.AddDays(-7);
        }

        public DateTime IncreaseDate(DateTime dateTime)
        {
            return dateTime.AddDays(7);
        }
    }
}
