using System;
using HaidaiTech.Notificator.Interfaces;

namespace tests.Interfaces
{
    public interface IMyOwnNotificationContextMessage
        : INotificationContextMessage
    {
        DateTime CreatedAt { get; }
        string MachineName { get; }
    }
}