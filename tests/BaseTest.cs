using Moq.AutoMock;
using Notificator.NotificationContextPattern;

namespace Notificator.Tests
{
    public abstract class BaseTest
    {
        protected NotificationContext notificationContext;

        protected BaseTest()
        {
            var mocker = new AutoMocker();

            notificationContext =
                mocker.CreateInstance<NotificationContext>();
        }
    }
}