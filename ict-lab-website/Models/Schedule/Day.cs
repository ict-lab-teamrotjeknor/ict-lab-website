using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Day
    {
        public int DayNumber { get; set; }
        public List<Hour> Hours { get; set; }
    }
}
