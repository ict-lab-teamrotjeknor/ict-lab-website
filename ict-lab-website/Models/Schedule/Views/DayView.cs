using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule.Views
{
    public class DayView : IView
    {
        public string Name => "ScheduleViewDay";

        public DateTime DecreaseDate(DateTime dateTime)
        {
            return dateTime.AddDays(-1);
        }

        public DateTime IncreaseDate(DateTime dateTime)
        {
            return dateTime.AddDays(1);
        }
    }
}
