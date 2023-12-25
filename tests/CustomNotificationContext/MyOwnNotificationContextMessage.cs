using System;
using HaidaiTech.Notificator.Interfaces;

namespace CustomNotificationContext.CustomNotificationContext
{

    public class MyOwnNotificationContextMessage
           : INotificationContextMessage
    {
        public DateTime CreatedAt { get; private set; }

        public string MachineName { get; private set; }

        public string Message { get; private set; }

        public string ErrorLevel { get; private set; }

        public string ErrorCode { get; private set; }

        public MyOwnNotificationContextMessage(
            DateTime createdAt,
            string machineName,
            string message,
            string errorLevel,
            string errorCode
        )
        {
            CreatedAt = createdAt;
            MachineName = machineName;
            Message = message;
            ErrorLevel = errorLevel;
            ErrorCode = errorCode;
        }

    }
}