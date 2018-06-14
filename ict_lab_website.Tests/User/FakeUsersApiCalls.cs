using System;
using ict_lab_website.Process;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Tests.User
{
	public class FakeUsersApiCalls : IApiCalls
    {
        public string GetRequest(string url)
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

        public JObject PostRequest(JObject postData, string url)
        {
            throw new NotImplementedException();
        }
    }
}
