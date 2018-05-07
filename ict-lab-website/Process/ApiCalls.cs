using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ict_lab_website.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Process
{
    public class ApiCalls
    {
        public ApiCalls()
        {
        }

        public JObject PostRequest(JObject postData, string sendUrl)
        {
            var result = new JObject();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(sendUrl);
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

            } catch (WebException e)
            {
                var test = e.Message;
                return e.Message;

            } catch (Exception ex) {
                
                var test = ex.Message;
                return ex.Message;
            }
        }
    }
}
