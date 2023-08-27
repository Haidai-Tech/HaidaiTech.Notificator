using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Notificator.NotificationContextPattern;

namespace Notificator.Interfaces
{
    public interface INotificationContext
    {

        void AddNotification(string message);
        void AddNotification(NotificationContextMessage message);
        Task AddNotificationAsync(string message);
        Task AddNotificationAsync(NotificationContextMessage message);
        void AddNotifications(IEnumerable<string> messages);
        void AddNotifications(IEnumerable<NotificationContextMessage> messages);
        Task AddNotificationsAsync(IEnumerable<string> messages);
        Task AddNotificationsAsync(IEnumerable<NotificationContextMessage> messages);
        bool HasNotifications();
        void ClearNotifications();
        IReadOnlyList<NotificationContextMessage> GetNotifications();
        Task<IReadOnlyList<NotificationContextMessage>> GetNotificationsAsync();
    }
}