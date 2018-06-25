using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Notifications
{
    public class FakeNotificationsApiCalls : IApiCalls
    {
		public string GetRequest(string url, string IdenticatieToken = null)
        {
            try
            {
                return System.IO.File.ReadAllText(@"TestData\notifications.json");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

		public JObject PostRequest(JObject postData, string url, string IdenticatieToken = null)
        {
            JObject jObject = new JObject();
            JToken jProperty = new JProperty("Result", "Succeed");
            jObject.Add(jProperty);

            return jObject;
        }
	}
}
