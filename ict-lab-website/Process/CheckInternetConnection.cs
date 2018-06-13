using System;
using System.Net;

namespace ict_lab_website.Process
{
    public static class CheckInternetConnection
    {
		public static bool CheckConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
