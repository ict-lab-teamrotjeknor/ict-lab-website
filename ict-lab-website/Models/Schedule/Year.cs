using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Year
    {
        public int YearNumber { get; set; }
        public List<Month> Months { get; set; }
    }
}
