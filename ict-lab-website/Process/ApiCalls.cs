using System;
using System.IO;
using System.Net;
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
    }
}
