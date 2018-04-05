using ict_lab_website.Process;
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
        private ApiCalls apiCalls = new ApiCalls();
        private string url = "http://145.24.222.103:8080/manage/getallrooms";

        public FakeRoomRepository()
        {
            //rooms = new List<Room>
            //{
            //    new Room
            //    {
            //        Id = 1,
            //        Name = "H.4.104",
            //        Reservations = new List<Reservation>
            //        {
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 05, 04, 12, 10, 0), Length = 2, Reserver = "MINUTO", Subject = "INFLAB-01" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 14, 00, 0), Length = 2, Reserver = "LAFLEJ", Subject = "TESTING" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 03, 05, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 08, 12, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" }
            //        }
            //    },
            //    new Room
            //    {
            //        Id = 2,
            //        Name = "H.4.204",
            //        Reservations = new List<Reservation>
            //        {
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2010, 04, 05, 11, 10, 0), Length = 2, Reserver = "COSTG", Subject = "INFDTA02-1" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2017, 05, 04, 12, 10, 0), Length = 2, Reserver = "MINUTO", Subject = "INFLAB-01" },
            //            new Reservation {RoomID = 1, DateAndTime = new DateTime(2018, 04, 05, 14, 00, 0), Length = 2, Reserver = "LAFLEJ", Subject = "TESTING" },
            //        },
            //    },
            //    new Room { Id = 3, Name = "H.3.304" },
            //    new Room { Id = 4, Name = "WD.3.304" },
            //    new Room { Id = 5, Name = "H.3.305" },
            //    new Room { Id = 6, Name = "WD.1.304" }
            //};
            rooms = apiCalls.GetAllRooms(url);
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

        public Room GetById(string id)
        {
            return rooms.Where(room => room.Id.Equals(id)).First();
        }

        public Room GetByName(string name)
        {
            return rooms.Where(room => room.Name.Equals(name)).First();
        }
    }
}
