﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule.Views
{
    public class YearView : IView
    {
        public string Name => "ScheduleViewYear";

        public DateTime DecreaseDate(DateTime dateTime)
        {
            return dateTime.AddYears(-1);
        }

        public DateTime IncreaseDate(DateTime dateTime)
        {
            return dateTime.AddYears(1);
        }
    }
}
