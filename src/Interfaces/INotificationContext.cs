using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaidaiTech.Notificator.Interfaces
{
    /// <summary>
    /// The Contract for Notification Context.
    /// </summary>
    /// <typeparam name="TMessage">TMessage represents the INotificationContextMessage</typeparam>
    public interface INotificationContext<TMessage>
    where TMessage : INotificationContextMessage
    {
        void AddNotification(TMessage message);
        Task AddNotificationAsync(TMessage message);
        void AddNotifications(IEnumerable<TMessage> messages);
        Task AddNotificationsAsync(IEnumerable<TMessage> messages);
        bool HasNotifications();
        IReadOnlyList<TMessage> GetNotifications();
        Task<IReadOnlyList<TMessage>> GetNotificationsAsync();
        void ClearNotifications();
    }

}