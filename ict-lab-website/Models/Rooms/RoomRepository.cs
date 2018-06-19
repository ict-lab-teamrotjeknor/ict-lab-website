using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IApiCalls apiCalls;

        public RoomRepository(IOptions<ApiConfig> apiConfig, ILogger<RoomsController> logger, IApiCalls apiCalls)
        {
            this.apiCalls = apiCalls;
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
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                logger.LogError($"{DateTime.Now} - [{file} : {line}] Cannot get rooms from API");
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
