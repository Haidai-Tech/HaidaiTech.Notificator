using System.Collections.Generic;
using Notificator.NotificationContextPattern;

namespace Notificator.Interfaces
{
    public interface INotificationContext
    {
        void AddNotification(NotificationContextMessages message);
        void AddNotifications(IEnumerable<NotificationContextMessages> messages);
        bool HasNotifications();
        IReadOnlyList<NotificationContextMessages> GetNotifications();
        void ClearNotifications();
    }
}