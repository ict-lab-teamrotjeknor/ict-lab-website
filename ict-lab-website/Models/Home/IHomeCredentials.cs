using System;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Home
{
    public interface IHomeCredentials
    {
		JObject LoginCredentials(JObject loginObject);
		JObject RegisterCredentials(JObject registerObject);
    }
}
