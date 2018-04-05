using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models
{
    public class APIConnection
    {
        private static string GetJsonFromApi(string url)
        {
            var json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString(url);
            }
            return json;
        }

        public static List<Room> GetAllRooms(string url)
        {
            List<Room> rooms = new List<Room>();
            var json = GetJsonFromApi(url);
            var classRooms = JObject.Parse(json)["Classroom"];

            foreach (JToken classrooms in classRooms)
            {
                foreach(JToken room in classrooms.Children())
                {
                    Room r = JsonConvert.DeserializeObject<Room>(room.ToString());
                    rooms.Add(r);
                }

            }
            return rooms;
        }
    }
}
