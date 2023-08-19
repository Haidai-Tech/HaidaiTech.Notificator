using System;
using System.Linq;
using System.Threading.Tasks;

using Moq.AutoMock;

using Notificator.Helpers;
using Notificator.NotificationContextPattern;
using Notificator.Tests.Concrete;
using Notificator.Tests.Customer;
using Notificator.Tests.Helpers;
using Notificator.Tests.Validator;

using FluentValidation.TestHelper;

using Xunit;
using System.Collections.Generic;
using FluentValidation;

namespace Notificator.Tests;

public class NotificatorTests
    : BaseTest
{
    [Fact]
    public void ShouldHaveNotificationsUsingHelpers()
    {
        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A normal notification",
                NotificationContextErrorLevelHelper.NORMAL,
                ErrorCodeTestHelper.ERROR_CODE_100
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "An attention notification",
                NotificationContextErrorLevelHelper.ATTENTION,
                ErrorCodeTestHelper.ERROR_CODE_101
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A critical notification",
                NotificationContextErrorLevelHelper.CRITICAL,
                ErrorCodeTestHelper.ERROR_CODE_102
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A panic notification",
                NotificationContextErrorLevelHelper.PANIC,
                ErrorCodeTestHelper.ERROR_CODE_103
            )
        );

        Assert.True(notificationContextConcrete.HasNotifications());
    }

    [Fact]
    public async Task ShouldHaveNotificationsUsingHelpersAsync()
    {

        await notificationContextConcrete.AddNotificationAsync(
            new NotificationContextMessage(
                "A normal notification",
                NotificationContextErrorLevelHelper.NORMAL,
                ErrorCodeTestHelper.ERROR_CODE_100
            )
        );

        await notificationContextConcrete.AddNotificationAsync(
            new NotificationContextMessage(
                "An Attention notification",
                NotificationContextErrorLevelHelper.ATTENTION,
                ErrorCodeTestHelper.ERROR_CODE_101
            )
        );

        await notificationContextConcrete.AddNotificationAsync(
            new NotificationContextMessage(
                "A critical notification",
                NotificationContextErrorLevelHelper.CRITICAL,
                ErrorCodeTestHelper.ERROR_CODE_102
            )
        );

        await notificationContextConcrete.AddNotificationAsync(
            new NotificationContextMessage(
                "A panic notification",
                NotificationContextErrorLevelHelper.PANIC,
                ErrorCodeTestHelper.ERROR_CODE_103
            )
        );

        var result = Task.Run(() => notificationContextConcrete.HasNotifications());

        Assert.True(result.Result);
    }

    [Theory]
    [InlineData("OMEGA_LEVEL", "ERR123456")]
    [InlineData("OMEGA_LEVEL", "123456")]
    public void ShouldHaveNotificationsUsingCustomizedErrorLevelAndErrorCode(
        string errorLevel,
        string errorCode
    )
    {

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A normal notification",
                errorLevel,
                errorCode
            )
        );

        Assert.True(notificationContextConcrete.HasNotifications()
        && notificationContextConcrete
                .GetNotifications()
                .Where(x => x.ErrorCode == errorCode &&
                            x.ErrorLevel == errorLevel)
                .ToList().Count == 1);
    }

    [Fact]
    public void ShouldClearNotifications()
    {

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "xptp"
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A normal notification",
                "NORMAL"
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "An Attention notification",
                "ATTENTION"
            )
        );

        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                "A critical notification",
                "CRITICAL"
            )
        );

        notificationContextConcrete.ClearNotifications();

        Assert.True(!notificationContextConcrete.HasNotifications() &&
                    notificationContextConcrete.GetNotifications().Count == 0);
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

        var result = Task.Run(() => !notificationContext.HasNotifications() &&
                                        notificationContext.GetNotificationsAsync().Result.Count == 0);

        Assert.True(result.Result);
    }

    [Theory]
    [InlineData("A normal notification")]
    [InlineData("A critical notification")]
    public void ShouldAddNotificationWithoutErrorCodeAndErrorLevel(
        string errorMessage
    )
    {
        notificationContextConcrete.AddNotification(
            new NotificationContextMessage(
                errorMessage
            )
        );

        Assert.True(notificationContextConcrete.HasNotifications()
        && notificationContextConcrete
                .GetNotifications()
                .Where(x => x.Message == errorMessage &&
                            x.ErrorLevel is null &&
                            x.ErrorCode is null)
                .ToList().Count == 1);

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

        List<NotificationContextMessage> _notificationContextMessage = new();

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

        notificationContextConcrete.AddNotifications(_notificationContextMessage);

        Assert.False(result.IsValid);
        Assert.True(notificationContextConcrete.HasNotifications());
    }
}


