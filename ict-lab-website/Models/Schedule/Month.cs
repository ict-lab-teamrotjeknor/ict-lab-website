using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Month
    {
        public int MonthNumber { get; set; }
        public List<Week> Weeks { get; set; }
    }
}
