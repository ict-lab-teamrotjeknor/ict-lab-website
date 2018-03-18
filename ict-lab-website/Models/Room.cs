using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomCode { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
