using System.Threading.Tasks;
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

            Assert.True(notificationContext.HasNotifications() &&
                        notificationContext.GetNotifications().Count == 3);
        }

        [Fact]
        public async Task ShouldHaveNotificationsAsync()
        {
            var mocker = new AutoMocker();

            var notificationContext =
                mocker.CreateInstance<NotificationContextConcrete>();

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A normal notification",
                    EnumNotificationContextErrorLevel.NORMAL
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.ATTENTION
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.CRITICAL
                )
            );

            var result = Task.Run(() => notificationContext.HasNotifications());

            Assert.True(result.Result);

        }

        [Fact]
        public void ShouldClearNotifications()
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

            notificationContext.ClearNotifications();

            Assert.True(!notificationContext.HasNotifications() &&
                        notificationContext.GetNotifications().Count == 0);
        }

        [Fact]
        public async Task ShouldClearNotificationsAsync()
        {
            var mocker = new AutoMocker();

            var notificationContext =
                mocker.CreateInstance<NotificationContextConcrete>();

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A normal notification",
                    EnumNotificationContextErrorLevel.NORMAL
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.ATTENTION
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    EnumNotificationContextErrorLevel.CRITICAL
                )
            );

            notificationContext.ClearNotifications();

            var result = Task.Run(() => !notificationContext.HasNotifications() &&
                                            notificationContext.GetNotificationsAsync().Result.Count == 0);

            Assert.True(result.Result);
        }
    }
}

