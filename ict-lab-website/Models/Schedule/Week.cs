using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Week
    {
        public int WeekNumber { get; set; }
        public List<Day> Days { get; set; }
    }
}
