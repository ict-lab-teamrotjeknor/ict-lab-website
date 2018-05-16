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
        private string url = "http://145.24.222.103:8080";
        string parametersGetAll = "/manage/getallrooms";

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                var json = ApiCalls.GetRequest(url + parametersGetAll);
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
