using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public class Repository
    {
        private static List<Room> rooms = new List<Room>();

        public static List<Room> Rooms
        {
            get
            {
                return rooms;
            }
        }

        public static void AddRoom(Room room)
        {
            rooms.Add(room);
        }
    }
}
