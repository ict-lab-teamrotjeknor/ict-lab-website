using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public class FakeRoomRepository : IRoomRepository
    {
        public static FakeRoomRepository SharedRepository { get; } = new FakeRoomRepository();
        private List<Room> rooms;

        public FakeRoomRepository()
        {
            rooms = new List<Room>
            {
                new Room
                {
                    ID = 1,
                    RoomCode = "H.4.104",
                    Reservations = new List<Reservation>
                    {
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 03, 26, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" }
                    }
                },
                new Room { ID = 2, RoomCode = "H.4.204" },
                new Room { ID = 3, RoomCode = "H.3.304" }
            };
        }


        public IQueryable<Room> Rooms => rooms.AsQueryable<Room>(); 
            

        public void Add(Room room)
        {
            rooms.Add(room);
        }

        public void Delete(Room room)
        {
            rooms.Remove(room);
        }

        public Room GetById(int id)
        {
            return rooms.Where(room => room.ID == id).First();
        }
    }
}
