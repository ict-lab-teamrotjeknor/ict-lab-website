using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;
        private readonly ILogger logger;

        public NotificationRepository(IApiCalls apiCalls, IOptions<ApiConfig> apiConfig, ILogger<NotificationsController> logger)
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
                var messages = JObject.Parse(json)["Messages"];
                foreach (var message in messages)
                {
                    Notification notification = JsonConvert.DeserializeObject<Notification>(message.ToString());
                    notifications.Add(notification);
                }

            }
            catch(Exception e)
            {
                logger.LogError("Cannot get notifications from API.", e, DateTime.Now);
            }

            return notifications;
        }

        public Boolean SendNotification(UploadableNotification notification)
        {
            var notificationJsonObject = (JObject)JToken.FromObject(notification);

            logger.LogInformation("Uploading notification to API..", DateTime.Now);
            var result = apiCalls.PostRequest(notificationJsonObject, apiConfig.Url + apiConfig.SendNotification);

            if (!result.HasValues)
            {
                logger.LogError("Uploading notification failed", DateTime.Now);
                return false;
            }

            return true;
        }

        public Boolean SendNotificationToGroup(UploadableGroupNotification notification)
        {
            var notificationJsonObject = (JObject)JToken.FromObject(notification);

            logger.LogInformation("Uploading notification to API..", DateTime.Now);
            var result = apiCalls.PostRequest(notificationJsonObject, apiConfig.Url + apiConfig.SendNotifiationToGroup);

            if (!result.HasValues)
            {
                logger.LogError("Uploading notification failed", DateTime.Now);
                return false;
            }

            return true;
        }
    }
}
