using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Fake_implementations
{
    public class FakeFailingApiCalls : IApiCalls
    {
		public string GetRequest(string url, string IdenticatieToken = null)
		{
			throw new Exception();
		}

		public JObject PostRequest(JObject postData, string url, string IdenticatieToken = null)
		{
			return new JObject();
		}
	}
}
