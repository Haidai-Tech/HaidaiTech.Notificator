using System.Collections.Generic;
using Notificator.Interfaces;

namespace Notificator.NotificationContextPattern
{
    public abstract class NotificationContext
        : INotificationContext
    {
        private readonly List<NotificationContextMessages> notifications
            = new List<NotificationContextMessages>();

        protected NotificationContext()
        { }
        
        public void AddNotification(NotificationContextMessages message)
        {
            notifications.Add(message);
        }

        public void AddNotifications(IEnumerable<NotificationContextMessages> messages)
        {
            notifications.AddRange(messages);
        }

        public bool HasNotifications()
        {
            return notifications.Count > 0;
        }

        public IReadOnlyList<NotificationContextMessages> GetNotifications()
        {
            return notifications.AsReadOnly();
        }

        public void ClearNotifications()
        {
            notifications.Clear();
        }
    }
}