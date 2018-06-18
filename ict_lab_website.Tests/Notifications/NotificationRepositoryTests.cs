using ict_lab_website.Controllers;
using ict_lab_website.Models;
using ict_lab_website.Models.Notifications;
using ict_lab_website.Process;
using ict_lab_website.Tests.Fake_implementations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ict_lab_website.Tests.Notifications
{
    public class NotificationRepositoryTests
    {
        private readonly ILogger<NotificationsController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls apiCalls;
        private readonly IApiCalls failingApiCalls;

        public NotificationRepositoryTests()
        {
            this.logger = new FakeLogger<NotificationsController>();
            this.apiConfig = new FakeIOptions();
            this.apiCalls = new FakeNotificationsApiCalls();
            this.failingApiCalls = new FakeFailingApiCalls();
        }

        [Fact]
        public void GetAll_ShouldWork()
        {
            INotificationRepository notificationRepository = new NotificationRepository(apiCalls, apiConfig, logger);

            List<Notification> notifications = notificationRepository.GetAll();

            Assert.NotEmpty(notifications);
            Assert.All(notifications, notification => Assert.IsType<Notification>(notification));
            Assert.All(notifications, notification => Assert.NotNull(notification.ID));
        }

        [Fact]
        void GetAll_ShouldReturnEmptyListWithNotWorkingApiCalls()
        {
            INotificationRepository notificationRepository = new NotificationRepository(failingApiCalls, apiConfig, logger);

            List<Notification> notification = notificationRepository.GetAll();

            Assert.Empty(notification);
        }

        [Fact]
        public void SendNotification_ShouldWorkWithWorkingApiCalls()
        {
            INotificationRepository notificationRepository = new NotificationRepository(apiCalls, apiConfig, logger);
            UploadableNotification notification = new UploadableNotification();
            Boolean expected = true;

            Boolean actual = notificationRepository.SendNotification(notification);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SendNotification_ShouldFailWithNotWorkingApiCalls()
        {
            INotificationRepository notificationRepository = new NotificationRepository(failingApiCalls, apiConfig, logger);
            UploadableNotification notification = new UploadableNotification();
            Boolean expected = false;

            Boolean actual = notificationRepository.SendNotification(notification);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SendNotificationToGroup_ShouldWorkWithWorkingApiCalls()
        {
            INotificationRepository notificationRepository = new NotificationRepository(apiCalls, apiConfig, logger);
            UploadableGroupNotification notification = new UploadableGroupNotification();
            Boolean expected = true;

            Boolean actual = notificationRepository.SendNotificationToGroup(notification);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SendNotificationToGroup_ShouldFailWithNotWorkingApiCalls()
        {
            INotificationRepository notificationRepository = new NotificationRepository(failingApiCalls, apiConfig, logger);
            UploadableGroupNotification notification = new UploadableGroupNotification();
            Boolean expected = false;

            Boolean actual = notificationRepository.SendNotificationToGroup(notification);

            Assert.Equal(expected, actual);
        }

    }
}
