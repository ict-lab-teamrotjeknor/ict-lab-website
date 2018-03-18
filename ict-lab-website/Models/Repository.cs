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

        public static Room GetRoom(int id)
        {
            Room room = rooms.FirstOrDefault(x => x.ID == id);
            return room; 
        }

        public static List<Room> GenerateExampleData()
        {
            for (int i = 1; i <= 10; i++)
            {
                AddRoom(new Room { ID = i, RoomCode = $"H4.40{i}" });
            }

            return Rooms;
        }
    }
}
