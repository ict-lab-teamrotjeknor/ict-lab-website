using ict_lab_website.Process;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    public class RoomRepository : IRepository<Room>
    {
        private List<Room> rooms;
        private RoomsApiCalls roomsApiCalls = new RoomsApiCalls();

        public RoomRepository()
        {
            rooms = roomsApiCalls.GetAll();
        }            

        public void Add(Room room)
        {
            rooms.Add(room);
        }

        public void Delete(Room room)
        {
            rooms.Remove(room);
        }

        public IQueryable<Room> GetAll()
        {
            return rooms.AsQueryable<Room>();
        }

        public Room GetById(string id)
        {
            return rooms.Where(room => room.Id.Equals(id)).First();
        }

        public Room GetByName(string name)
        {
           return rooms.Where(r => r.Name.Equals(name)).First()?? new Room();
        }
    }
}
