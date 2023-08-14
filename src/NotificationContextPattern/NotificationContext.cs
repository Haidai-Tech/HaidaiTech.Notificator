using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Notificator.Interfaces;

namespace Notificator.NotificationContextPattern
{
    public abstract class NotificationContext
        : INotificationContext
    {
        private readonly List<NotificationContextMessage> _notifications
            = new List<NotificationContextMessage>();

        protected NotificationContext()
        { }

        /// <summary>
        /// Add a Notification Message in a Notification Context
        /// </summary>
        /// <param name="message">NotificationContextMessage</param>
        public void AddNotification(NotificationContextMessage message)
        {
            _notifications.Add(message);
        }

        /// <summary>
        /// Add a list of Notification Message
        /// </summary>
        /// <param name="messages"></param>
        public void AddNotifications(IEnumerable<NotificationContextMessage> messages)
        {
            _notifications.AddRange(messages);
        }

        /// <summary>
        /// Verify if exists notications 
        /// </summary>
        /// <returns></returns>
        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }

        /// <summary>
        /// Get notifications stored
        /// </summary>
        /// <returns>IReadOnlyList of Notifications</returns>
        public IReadOnlyList<NotificationContextMessage> GetNotifications()
        {
            return _notifications.AsReadOnly();
        }

        /// <summary>
        /// Clear Notification Messages
        /// </summary>
        public void ClearNotifications()
        {
            _notifications.Clear();
        }

        public async Task AddNotificationAsync(NotificationContextMessage message)
            => await Task.Run(() => { _notifications.Add(message); });

        public async Task AddNotificationsAsync(IEnumerable<NotificationContextMessage> messages)
            => await Task.Run(() => { _notifications.AddRange(messages); });

        public IReadOnlyList<NotificationContextMessage> GetNotifications(Expression<Func<NotificationContextMessage, bool>> predicate)
        {
            if (predicate != null)
            {
                return _notifications.Where(predicate).
            }

        }

        public Task<IReadOnlyList<NotificationContextMessage>> GetNotificationsAsync(Expression<Func<NotificationContextMessage, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}