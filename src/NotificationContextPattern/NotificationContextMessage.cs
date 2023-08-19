namespace Notificator.NotificationContextPattern;

public sealed class NotificationContextMessage
{
    /// <summary>
    /// The message of notification.
    /// </summary>
    /// <value>string</value>
    public string Message { get; private set; } = default!;

    /// <summary>
    /// A string Notification Criticality level.
    /// </summary>
    /// <value>string</value>
    public string ErrorLevel { get; private set; } = default!;

    /// <summary>
    /// The error code of notification.
    /// </summary>
    /// <value>string</value>
    public string ErrorCode { get; private set; } = default!;

    public NotificationContextMessage(
        string message,
        string notificationContextErrorLevel = null,
        string errorCode = null
    )
    {
        Message = message;
        ErrorLevel = notificationContextErrorLevel;
        ErrorCode = errorCode;
    }
}
