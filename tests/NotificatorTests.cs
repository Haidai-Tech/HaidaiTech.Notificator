using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomNotificationContext.CustomNotificationContext;
using FluentValidation;
using HaidaiTech.Notificator.Helpers;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;
using HaidaiTech.Notificator.Tests.Customer;
using HaidaiTech.Notificator.Tests.Helpers;
using HaidaiTech.Notificator.Tests.Validator;
using Xunit;

namespace HaidaiTech.Notificator.Tests
{
    public class NotificatorTests
        : BaseTest
    {
        [Fact]
        public void ShouldHaveNotificationsUsingHelpers()
        {
            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A normal notification",
                    NotificationContextErrorLevelHelper.NORMAL,
                    ErrorCodeTestHelper.ERROR_CODE_100
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "An attention notification",
                    NotificationContextErrorLevelHelper.ATTENTION,
                    ErrorCodeTestHelper.ERROR_CODE_101
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A critical notification",
                    NotificationContextErrorLevelHelper.CRITICAL,
                    ErrorCodeTestHelper.ERROR_CODE_102
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A panic notification",
                    NotificationContextErrorLevelHelper.PANIC,
                    ErrorCodeTestHelper.ERROR_CODE_103
                )
            );

            Assert.True(notificationContext.HasNotifications());
        }

        [Fact]
        public async Task ShouldHaveNotificationsUsingHelpersAsync()
        {

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A normal notification",
                    NotificationContextErrorLevelHelper.NORMAL,
                    ErrorCodeTestHelper.ERROR_CODE_100
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "An Attention notification",
                    NotificationContextErrorLevelHelper.ATTENTION,
                    ErrorCodeTestHelper.ERROR_CODE_101
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    NotificationContextErrorLevelHelper.CRITICAL,
                    ErrorCodeTestHelper.ERROR_CODE_102
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A panic notification",
                    NotificationContextErrorLevelHelper.PANIC,
                    ErrorCodeTestHelper.ERROR_CODE_103
                )
            );

            var result = await Task.Run(() => notificationContext.HasNotifications());

            Assert.True(result);
        }

        [Theory]
        [InlineData("OMEGA_LEVEL", "ERR123456")]
        [InlineData("OMEGA_LEVEL", "123456")]
        public void ShouldHaveNotificationsUsingCustomizedErrorLevelAndErrorCode(
            string errorLevel,
            string errorCode
        )
        {

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A normal notification",
                    errorLevel,
                    errorCode
                )
            );

            Assert.True(notificationContext.HasNotifications()
            && notificationContext
                    .GetNotifications()
                    .Where(x => x.ErrorCode == errorCode &&
                                x.ErrorLevel == errorLevel)
                    .ToList().Count == 1);
        }

        [Fact]
        public void ShouldClearNotifications()
        {

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "xptp"
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A normal notification",
                    "NORMAL"
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "An Attention notification",
                    "ATTENTION"
                )
            );

            notificationContext.AddNotification(
                new NotificationContextMessage(
                    "A critical notification",
                    "CRITICAL"
                )
            );

            notificationContext.ClearNotifications();

            Assert.True(!notificationContext.HasNotifications() &&
                        notificationContext.GetNotifications().Count == 0);
        }

        [Fact]
        public async Task ShouldClearNotificationsAsync()
        {
            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A normal notification",
                    "NORMAL"
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "An Attention notification",
                    "ATTENTION"
                )
            );

            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(
                    "A critical notification",
                    "CRITICAL"
                )
            );

            notificationContext.ClearNotifications();

            var notificationResult = await notificationContext.GetNotificationsAsync();

            var result = Task.Run(() => !notificationContext.HasNotifications() &&
                                            notificationResult.Count == 0);
        }

        [Theory]
        [InlineData("A normal notification")]
        [InlineData("A critical notification")]
        public void ShouldAddNotificationWithoutErrorCodeAndErrorLevel(
            string errorMessage
        )
        {
            notificationContext.AddNotification(
                new NotificationContextMessage(
                    errorMessage
                )
            );

            Assert.True(notificationContext.HasNotifications()
            && notificationContext
                    .GetNotifications()
                    .Where(x => x.Message == errorMessage &&
                                x.ErrorLevel is null &&
                                x.ErrorCode is null)
                    .ToList().Count == 1);

        }

        [Theory]
        [InlineData("A normal notification")]
        [InlineData("A critical notification")]
        public void ShouldAddSingleMessageInNotificationContext(
            string errorMessage
        )
        {
            notificationContext.AddNotification(
                new NotificationContextMessage(errorMessage)
            );

            Assert.True(notificationContext.HasNotifications()
            && notificationContext
                    .GetNotifications()
                    .Where(x => x.Message == errorMessage &&
                                x.ErrorLevel is null &&
                                x.ErrorCode is null)
                    .ToList().Count == 1);

        }

        [Theory]
        [InlineData("A normal notification")]
        [InlineData("A critical notification")]
        public async Task ShouldAddSingleMessageInNotificationContextAsync(
            string errorMessage
        )
        {
            await notificationContext.AddNotificationAsync(
                new NotificationContextMessage(errorMessage)
            );

            Assert.True(notificationContext.HasNotifications()
            && notificationContext
                    .GetNotifications()
                    .Where(x => x.Message == errorMessage &&
                                x.ErrorLevel is null &&
                                x.ErrorCode is null)
                    .ToList().Count == 1);

        }

        [Fact]
        public void ShouldAddListOfStringToNotificationContext()
        {
            List<NotificationContextMessage> notifications
                = new List<NotificationContextMessage>()
        {
            new NotificationContextMessage("Notification 1"),
            new NotificationContextMessage("Notification 2")
        };

            notificationContext.AddNotifications(
                notifications
            );

            Assert.True(notificationContext.HasNotifications()
                        && notificationContext.GetNotifications().Count == notifications.Count);
        }

        [Fact]
        public async Task ShouldAddListOfStringToNotificationContextAsync()
        {
            List<NotificationContextMessage> notifications = new List<NotificationContextMessage>()
        {
            new NotificationContextMessage("Notification 1"),
            new NotificationContextMessage("Notification 2")
        };

            await notificationContext.AddNotificationsAsync(
                notifications
            );

            Assert.True(notificationContext.HasNotifications()
                        && notificationContext.GetNotifications().Count == notifications.Count);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldToUseFluentValidatorData(string name)
        {
            var customer = new CustomerTest(
                Guid.NewGuid(),
                name
            );

            var validator = new CustomerTestValidator();

            var result = validator.Validate(customer);

            List<NotificationContextMessage> _notificationContextMessage
                = new List<NotificationContextMessage>();

            result.Errors.ForEach(x =>
                {
                    _notificationContextMessage.Add(
                        new NotificationContextMessage(
                            x.ErrorMessage,
                            typeof(Severity).GetEnumName(x.Severity),
                            x.ErrorCode
                        )
                    );
                }
            );

            notificationContext.AddNotifications(_notificationContextMessage);

            Assert.False(result.IsValid);
            Assert.True(notificationContext.HasNotifications());
        }

        [Fact]

        public void ShouldCreateMyOwnContextMessage()
        {

            var message = new MyOwnNotificationContextMessage(
                DateTime.Now,
                "Machine 1",
                "Error Message",
                "myCustomErrorLevel",
                "myCustonErrorCode"
            );

            var customNotificationContext = new NotificationContext<MyOwnNotificationContextMessage>();

            customNotificationContext.AddNotification(message);

            var messageInContext = notificationContext.GetNotifications().FirstOrDefault();

            Assert.True(messageInContext.GetType().Equals(typeof(MyOwnNotificationContextMessage)));
        }


        [Fact]
        public void ShouldCreateMyOwnContextMessageAndUseLinqToFilterData()
        {
            var message = new MyOwnNotificationContextMessage(
                DateTime.Now,
                "Machine 1",
                "Error Message",
                "myCustomErrorLevel",
                "myCustonErrorCode"
            );

            var message2 = new MyOwnNotificationContextMessage(
                DateTime.Now,
                "Machine xpto",
                "Error Message",
                "myCustomErrorLevel",
                "myCustonErrorCode"
            );

            var customNotificationContext = new NotificationContext<MyOwnNotificationContextMessage>();

            customNotificationContext.AddNotification(message);
            customNotificationContext.AddNotification(message2);

            Assert.True(customNotificationContext.HasNotifications()
            && customNotificationContext
                    .GetNotifications()
                    .Where(x => x.MachineName == "Machine 1")
                    .ToList().Count == 1);
        }
    }
}