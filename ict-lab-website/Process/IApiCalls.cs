using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Process
{
    public interface IApiCalls
    {
        JObject PostRequest(JObject postData, string url, string IdenticatieToken = null);
        string GetRequest(string url);
    }
}
