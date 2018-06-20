using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using ict_lab_website.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Process
{
    public class ApiCalls : IApiCalls
    {
        //public HttpResponse httpResponse;
        public JObject PostRequest(JObject postData, string url)
        {
            var result = new JObject();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "Post";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var test = httpResponse.Headers.Get(6);

                    //Cookie cookie = new Cookie();
                    //cookie.Name = "hoi";
                    //httpResponse.Cookies.Add(cookie);
                    //tester = httpContextAccessor.HttpContext.Request.Cookies[test];
                    result = JObject.Parse(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                return new JObject();
            }

            return result;
        }

        public string GetRequest(string url)
        {
            var json = "";

            try 
            {
                using (var client = new WebClient())
                {
                    json = client.DownloadString(url);
                }
                return json;

            }
            catch (WebException e)
            {
                throw e;
            }
        }
    }
}
