using ict_lab_website.Process;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Notifications
{
    public class NotificationRepository
    {
        private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;

        public NotificationRepository(IApiCalls apiCalls, IOptions<ApiConfig> apiConfig)
        {
            this.apiCalls = apiCalls;
            this.apiConfig = apiConfig.Value;
        }

        public List<Notification> GetAll()
        {
            List<Notification> notifications = new List<Notification>();

            try
            {
                var json = apiCalls.GetRequest(apiConfig.Url + apiConfig.GetAllNotifications);

                
            }
            catch(Exception e)
            {
                throw e; 
            }

            return notifications;
        }

        public void SendNotification(string message, string receiverUsername)
        {

        }

        public void SendNotificationToGroup(string message, string role)
        {

        }
    }
}
