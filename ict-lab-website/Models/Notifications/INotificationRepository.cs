using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Notifications
{
    public interface INotificationRepository
    {
        List<Notification> GetAll(string token);
        Boolean SendNotification(UploadableNotification notification);
        Boolean SendNotificationToGroup(UploadableGroupNotification notification);
    }
}
