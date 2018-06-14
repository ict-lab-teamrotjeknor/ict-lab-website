using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;

        public NotificationRepository(IApiCalls apiCalls, IOptions<ApiConfig> apiConfig, ILogger logger)
        {
            this.apiCalls = apiCalls;
            this.apiConfig = apiConfig.Value;
            this.logger = logger;
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
                logger.LogError("Cannot get notifications from API.", e, DateTime.Now);
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
