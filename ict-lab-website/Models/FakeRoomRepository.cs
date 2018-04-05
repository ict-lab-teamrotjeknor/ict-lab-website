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
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 05, 04, 12, 10, 0), Length = 2, Reserver = "MINUTO", Subject = "INFLAB-01" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 14, 00, 0), Length = 2, Reserver = "LAFLEJ", Subject = "TESTING" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 03, 05, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 08, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" }
                    }
                },
                new Room
                {
                    ID = 2,
                    RoomCode = "H.4.204",
                    Reservations = new List<Reservation>
                    {
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2010, 04, 05, 11, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2017, 05, 04, 12, 10, 0), Length = 2, Reserver = "MINUTO", Subject = "INFLAB-01" },
                        new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 14, 00, 0), Length = 2, Reserver = "LAFLEJ", Subject = "TESTING" },
                    },
                },
                new Room { ID = 3, RoomCode = "H.3.304" },
                new Room { ID = 4, RoomCode = "WD.3.304" },
                new Room { ID = 5, RoomCode = "H.3.305" },
                new Room { ID = 6, RoomCode = "WD.1.304" }
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
