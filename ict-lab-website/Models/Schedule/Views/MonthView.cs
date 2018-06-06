using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule.Views
{
    public class MonthView : IView
    {
        public string Name => "ScheduleViewMonth";

        public DateTime DecreaseDate(DateTime dateTime)
        {
            return dateTime.AddMonths(-1);
        }

        public DateTime IncreaseDate(DateTime dateTime)
        {
            return dateTime.AddMonths(1);
        }
    }
}
