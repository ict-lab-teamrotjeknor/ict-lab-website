using ict_lab_website.Models.Rooms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Process
{
    public class RoomsApiCalls
    {
        private ApiCalls ApiCalls = new ApiCalls();
        private ApiConfig ApiConfig;

        public RoomsApiCalls(ApiConfig apiConfig)
        {
            this.ApiConfig = apiConfig;
        }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                var json = ApiCalls.GetRequest(ApiConfig.Url + ApiConfig.GetAllRooms);
                var classRooms = JObject.Parse(json)["Classroom"];

                foreach (JToken classroom in classRooms)
                {
                    foreach (JToken room in classroom.Children())
                    {
                        Room r = JsonConvert.DeserializeObject<Room>(room.ToString());
                        rooms.Add(r);
                    }
                }
                return rooms;
            }
            catch (Exception e)
            {
                return rooms;
            }
        }
    }
}
