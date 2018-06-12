using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Schedule
{
    class FakeScheduleApiCalls : IApiCalls
    {
        public string GetRequest(string url)
        {
            try
            {
                return System.IO.File.ReadAllText(@"\TestData\getweek.json");
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public JObject PostRequest(JObject postData, string url)
        {
            JObject jObject = new JObject();
            JToken jProperty = new JProperty("Result", "Succeeded");
            jObject.Add(jProperty);

            return jObject;
        }
    }
}
