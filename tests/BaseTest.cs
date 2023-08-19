using Moq.AutoMock;
using Notificator.Tests.Concrete;

namespace Notificator.Tests
{
    public abstract class BaseTest
    {
        protected NotificationContextConcrete notificationContextConcrete;

        protected BaseTest()
        {
            var mocker = new AutoMocker();

            notificationContextConcrete =
                mocker.CreateInstance<NotificationContextConcrete>();
        }
    }
}