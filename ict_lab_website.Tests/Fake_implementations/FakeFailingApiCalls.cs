using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Fake_implementations
{
    public class FakeFailingApiCalls : IApiCalls
    {
        public string GetRequest(string url)
        {
            throw new Exception();
        }

        public JObject PostRequest(JObject postData, string url)
        {
            return new JObject();
        }
    }
}
