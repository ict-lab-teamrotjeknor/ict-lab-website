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
		public JObject PostRequest(JObject postData, string url, string IdenticatieToken = null)
        {
            var result = new JObject();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
				httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.Method = "Post";

				if(!string.IsNullOrEmpty(IdenticatieToken)){
					CookieContainer container = new CookieContainer();
					Cookie IdenticatieCookie = new Cookie();
					IdenticatieCookie.Expires = DateTime.Now.AddDays(1);
					IdenticatieCookie.Name = ".AspNetCore.Identity.Application";
					IdenticatieCookie.Value = IdenticatieToken;
					IdenticatieCookie.Domain = "145.24.222.103";
					IdenticatieCookie.Path = "/";

					httpWebRequest.CookieContainer.Add(IdenticatieCookie);
				}

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				string cookie = string.Empty;

				try{
					cookie = httpResponse.Headers.Get("Set-Cookie");
				} catch(Exception e){
					cookie = string.Empty;
				}

				if(!string.IsNullOrEmpty(cookie)){
					if(cookie.Contains(".AspNetCore.Identity.Application")){
						var token = cookie.Split(";");
						var identicatie = token[0].Split("=");
						UserObject.login = identicatie[1];
					}
				}

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

        public string GetRequest(string url, string IdenticatieToken = null)
        {
            var json = "";

            try 
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.Cookie, ".AspNetCore.Identity.Application=" + IdenticatieToken);
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
