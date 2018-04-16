using ict_lab_website.Process;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    public class ApiRoomRepository : IRoomRepository
    {
        private List<Room> rooms;
        private ApiCalls apiCalls = new ApiCalls();
        private string url = "http://145.24.222.103:8080/manage/getallrooms";

        public ApiRoomRepository()
        {
            rooms = GetRoomsFromApi(url);
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

        private List<Room> GetRoomsFromApi(string url)
        {
            List<Room> rooms = new List<Room>();
            var json = apiCalls.GetRequest(url);
            var classRooms = JObject.Parse(json)["Classroom"];

            foreach (JToken classrooms in classRooms)
            {
                foreach (JToken room in classrooms.Children())
                {
                    Room r = JsonConvert.DeserializeObject<Room>(room.ToString());
                    rooms.Add(r);
                }

            }
            return rooms;
        }
    }
}
