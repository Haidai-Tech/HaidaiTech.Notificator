using Moq.AutoMock;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;

namespace HaidaiTech.Notificator.Tests
{
    public abstract class BaseTest
    {
        protected NotificationContext<NotificationContextMessage> notificationContext;
        protected BaseTest()
        {
            var mocker = new AutoMocker();

            notificationContext =
                mocker.CreateInstance<NotificationContext<NotificationContextMessage>>();
        }
    }
}