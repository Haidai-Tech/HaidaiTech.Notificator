using Notificator.Enums;

namespace Notificator.NotificationContextPattern
{
    public sealed class NotificationContextMessage
    {
        /// <summary>
        /// The message of notification
        /// </summary>
        /// <value></value>
        public string Message { get; private set; } = string.Empty;

        /// <summary>
        /// Notification Cricity level
        /// </summary>
        /// <value></value>
        public EnumNotificationContextErrorLevel NotificationContextErrorLevel { get; private set; }

        public NotificationContextMessage(
            string message,
            EnumNotificationContextErrorLevel notificationContextErrorLevel
        )
        {
            Message = message;
            NotificationContextErrorLevel = notificationContextErrorLevel;
        }
    }
}