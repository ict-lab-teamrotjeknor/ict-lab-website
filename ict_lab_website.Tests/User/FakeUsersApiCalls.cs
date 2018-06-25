using System;
using ict_lab_website.Process;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Tests.User
{
	public class FakeUsersApiCalls : IApiCalls
    {
		public string GetRequest(string url, string IdenticatieToken = null)
        {
            try
            {
                return System.IO.File.ReadAllText(@"\TestData\getallusers.json");
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
