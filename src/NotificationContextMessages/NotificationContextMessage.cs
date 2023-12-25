using HaidaiTech.Notificator.Interfaces;

namespace HaidaiTech.Notificator.NotificationContextMessages
{
    /// <summary>
    /// The default NotificationContextMessage class
    /// </summary>
    public sealed class NotificationContextMessage
        : INotificationContextMessage
    {

        /// <summary>
        /// The message of a notification.
        /// </summary>
        /// <value>string</value>
        public string Message { get; private set; } = default!;

        /// <summary>
        /// A string Notification Criticality level.
        /// </summary>
        /// <value>string</value>
        public string ErrorLevel { get; private set; } = default!;

        /// <summary>
        /// The error code of a notification.
        /// </summary>
        /// <value>string</value>
        public string ErrorCode { get; private set; } = default!;

        public NotificationContextMessage(
            string message,
            string errorLevel = null,
            string errorCode = null
        )
        {
            Message = message;
            ErrorLevel = errorLevel;
            ErrorCode = errorCode;
        }
    }
}