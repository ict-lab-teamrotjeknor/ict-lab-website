using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    public class RoomRepository : IRepository<Room>
    {
        private List<Room> rooms;
        private readonly ApiConfig apiConfig;
        private readonly ILogger logger;
        private readonly ApiCalls apiCalls;

        public RoomRepository(IOptions<ApiConfig> apiConfig, ILogger<RoomsController> logger)
        {
            apiCalls = new ApiCalls();
            this.apiConfig = apiConfig.Value;
            this.logger = logger;
            rooms = GetAll();
        }            

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                var json = apiCalls.GetRequest(apiConfig.Url + apiConfig.GetAllRooms);
                var classRooms = JObject.Parse(json)["Classroom"];

                foreach (JToken classroom in classRooms)
                {
                    foreach (JToken room in classroom.Children())
                    {
                        Room r = JsonConvert.DeserializeObject<Room>(room.ToString());
                        rooms.Add(r);
                    }
                }
            }
            catch (WebException e)
            {
                logger.LogError("Cannot get rooms from API", e, DateTime.Now);
            }

            return rooms;
        }

        public Room GetById(string id)
        {
            return rooms.Where(room => room.Id.Equals(id)).First()?? new Room();
        }

        public Room GetByName(string name)
        {
           return rooms.Where(r => r.Name.Equals(name)).First()?? new Room();
        }
    }
}
