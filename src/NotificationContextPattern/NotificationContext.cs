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

        public async Task AddNotificationAsync(TMessage message)
            => await Task.Run(() => { _notifications.Add(message); });

        public void AddNotifications(IEnumerable<TMessage> messages)
            => _notifications.AddRange(messages);

        public async Task AddNotificationsAsync(IEnumerable<TMessage> messages)
            => await Task.Run(() => { _notifications.AddRange(messages); });

        public bool HasNotifications()
            => _notifications.Count > 0;

        public IReadOnlyList<TMessage> GetNotifications()
            => _notifications.AsReadOnly();

        public async Task<IReadOnlyList<TMessage>> GetNotificationsAsync()
            => await Task.Run(() => _notifications.AsReadOnly());
        public void ClearNotifications()
            => _notifications.Clear();
    }
}