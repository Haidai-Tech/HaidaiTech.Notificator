using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaidaiTech.Notificator.Interfaces
{
    /// <summary>
    /// Defines the contract for the Notification Context.
    /// </summary>
    /// <typeparam name="TMessage">The type representing the INotificationContextMessage.</typeparam>
    public interface INotificationContext<TMessage>
        where TMessage : INotificationContextMessage
    {

        /// <summary>
        /// Adds a notification message to the NotificationContext.
        /// </summary>
        /// <param name="message">The notification context message to add.</param>

        void AddNotification(TMessage message);

        /// <summary>
        /// Asynchronously adds a notification message to the NotificationContext.
        /// </summary>
        /// <param name="message">The notification context message to add.</param>

        Task AddNotificationAsync(TMessage message);

        /// <summary>
        /// Adds a list of notification messages to the NotificationContext.
        /// </summary>
        /// <param name="messages">The list of notification messages to add.</param>
        void AddNotifications(IEnumerable<TMessage> messages);

        /// <summary>
        /// Asynchronously adds a list of notification messages to the NotificationContext.
        /// </summary>
        /// <param name="messages">The list of notification messages to add.</param>
        Task AddNotificationsAsync(IEnumerable<TMessage> messages);

        /// <summary>
        /// Gets a value indicating whether there are notification context messages.
        /// </summary>
        /// <returns>True if there are notification context messages; otherwise, false.</returns>
        bool HasNotifications();

        /// <summary>
        /// Retrieves the notification messages.
        /// </summary>
        /// <returns>An IReadOnlyList of notification messages.</returns>
        IReadOnlyList<TMessage> GetNotifications();

        /// <summary>
        /// Asynchronously retrieves the notification messages.
        /// </summary>
        /// <returns>An IReadOnlyList of notification messages.</returns>
        Task<IReadOnlyList<TMessage>> GetNotificationsAsync();

        /// <summary>
        /// Clear Notification Context Messages
        /// </summary>
        void ClearNotifications();
    }

}