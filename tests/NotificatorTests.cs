using Moq.AutoMock;
using Notificator.Enums;
using Notificator.NotificationContextPattern;
using Notificator.Tests.Concrete;
using Xunit;

namespace Notificator.Tests
{
    public class NotificatorTests
    {
        [Fact]
        public void ShouldHaveNotifications()
        {
            var mocker = new AutoMocker();

            var notificationContext =
                mocker.CreateInstance<NotificationContextConcrete>();

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A normal notification",
                    EnumNotificationContextErrorLevel.NORMAL
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.ATTENTION
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.CRITICAL
                )
            );

            notificationContext.GetNotifications()


        }
    }
}

