using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                logger.LogError($"{DateTime.Now} - [{file} : {line}] Cannot get notifications from API");
            }

            return notifications;
        }

        public Boolean SendNotification(UploadableNotification notification)
        {
            var notificationJsonObject = (JObject)JToken.FromObject(notification);

            logger.LogInformation($"{DateTime.Now} - Uploading notification to API.");
			var result = apiCalls.PostRequest(notificationJsonObject, apiConfig.Url + apiConfig.SendNotification);

            if (!result.HasValues)
            {
                logger.LogWarning($"{DateTime.Now} - Uploading notification failed");
                return false;
            }

            return true;
        }

        public Boolean SendNotificationToGroup(UploadableGroupNotification notification)
        {
            var notificationJsonObject = (JObject)JToken.FromObject(notification);

            logger.LogInformation($"{DateTime.Now} - Uploading notification to API..");
			var result = apiCalls.PostRequest(notificationJsonObject, apiConfig.Url + apiConfig.SendNotificationToGroup);

            if (!result.HasValues)
            {
                logger.LogWarning($"{DateTime.Now} - Uploading notification failed");
                return false;
            }

            return true;
        }
    }
}
