using System.Collections.Generic;
using System.Threading.Tasks;
using HaidaiTech.Notificator.Interfaces;

namespace HaidaiTech.Notificator.NotificationContextPattern
{

    /// <summary>
    /// Represents the Notification Context, with TMessage defining the implementation of INotificationContextMessage.
    /// </summary>
    /// <typeparam name="TMessage">The class implementing the INotificationContextMessage interface.</typeparam>

    public sealed class NotificationContext<TMessage>
    : INotificationContext<TMessage>
    where TMessage : INotificationContextMessage
    {
        private readonly List<TMessage> _notifications = new List<TMessage>();

        public void AddNotification(TMessage message)
            => _notifications.Add(message);

        public Task AddNotificationAsync(TMessage message)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public void AddNotifications(IEnumerable<TMessage> messages)
            => _notifications.AddRange(messages);

        public Task AddNotificationsAsync(IEnumerable<TMessage> messages)
        {
            _notifications.AddRange(messages);
            return Task.CompletedTask;
        }

        public bool HasNotifications()
            => _notifications.Count > 0;

        public IReadOnlyList<TMessage> GetNotifications()
            => _notifications.AsReadOnly();

        public Task<IReadOnlyList<TMessage>> GetNotificationsAsync()
            => Task.FromResult((IReadOnlyList<TMessage>)_notifications.AsReadOnly());

        public void ClearNotifications()
            => _notifications.Clear();
    }

}
