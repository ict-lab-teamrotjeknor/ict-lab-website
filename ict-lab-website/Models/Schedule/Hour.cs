using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule
{
    public class Hour
    {
        public int HourNumber { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
