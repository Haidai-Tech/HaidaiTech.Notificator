using Notificator.Enums;

namespace Notificator.NotificationContextPattern
{
    public class NotificationContextMessages
    {
        public NotificationContextMessages(
            string message,
            EnumNotificationContextErrorLevel notificationContextErrorLevel
        )
        {
            Message = message;
            NotificationContextErrorLevel = notificationContextErrorLevel;
        }

        public string Message { get; private set; } = string.Empty;

        public EnumNotificationContextErrorLevel NotificationContextErrorLevel { get; private set; }
    }
}