using System.Collections.Generic;
using System.Threading.Tasks;
using Notificator.Interfaces;

namespace Notificator.NotificationContextPattern;

public sealed class NotificationContext
    : INotificationContext
{
    private readonly List<NotificationContextMessage> _notifications = new();

    /// <summary>
    /// Add a Notification Message in a Notification Context
    /// </summary>
    /// <param name="message">NotificationContextMessage</param>
    public void AddNotification(NotificationContextMessage message)
        => _notifications.Add(message);

    /// <summary>
    /// Add a Notification Message in a Notification Context
    /// </summary>
    /// <param name="message">NotificationContextMessage</param>
    public async Task AddNotificationAsync(NotificationContextMessage message)
        => await Task.Run(() => { _notifications.Add(message); });

    /// <summary>
    /// Add a list of Notification Message
    /// </summary>
    /// <param name="messages">IEnumerable<NotificationContextMessage></param>
    public void AddNotifications(IEnumerable<NotificationContextMessage> messages)
        => _notifications.AddRange(messages);

    /// <summary>
    /// Add a list of Notification Message
    /// </summary>
    /// <param name="messages">IEnumerable<NotificationContextMessage></param>
    public async Task AddNotificationsAsync(IEnumerable<NotificationContextMessage> messages)
        => await Task.Run(() => { _notifications.AddRange(messages); });

    /// <summary>
    /// Verify if exists notications 
    /// </summary>
    /// <returns></returns>
    public bool HasNotifications()
        => _notifications.Count > 0;

    /// <summary>
    /// Get notifications stored
    /// </summary>
    /// <returns>IReadOnlyList of Notifications</returns>
    public IReadOnlyList<NotificationContextMessage> GetNotifications()
        => _notifications.AsReadOnly();

    public async Task<IReadOnlyList<NotificationContextMessage>> GetNotificationsAsync()
        => await Task.Run(() => { return _notifications.AsReadOnly(); });

    /// <summary>
    /// Clear Notification Messages
    /// </summary>
    public void ClearNotifications()
        => _notifications.Clear();
}
