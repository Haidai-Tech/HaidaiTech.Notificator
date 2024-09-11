using HaidaiTech.Notificator.Interfaces;

namespace HaidaiTech.Notificator.NotificationContextMessages
{
    /// <summary>
    /// Represents the default implementation of a notification message in the Notification Context.
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

        private NotificationContextMessage(
            string message,
            string errorLevel = null,
            string errorCode = null
        )
        {
            Message = message;
            ErrorLevel = errorLevel;
            ErrorCode = errorCode;
        }
        public static NotificationContextMessage Create(
            string message,
            string errorLevel = null,
            string errorCode = null
        )
        {
            return new NotificationContextMessage(
                message,
                errorLevel,
                errorCode
            );
        }
    }
}