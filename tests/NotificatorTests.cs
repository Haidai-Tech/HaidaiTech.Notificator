using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Notificator.Helpers;
using Notificator.NotificationContextPattern;
using Notificator.Tests.Customer;
using Notificator.Tests.Helpers;
using Notificator.Tests.Validator;
using Xunit;

namespace Notificator.Tests;

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

        var result = Task.Run(() => notificationContext.HasNotifications());

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
            errorMessage
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
            errorMessage
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
        List<string> notifications = new()
        {
            "Notification 1",
            "Notification 2"
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
        List<string> notifications = new()
        {
            "Notification 1",
            "Notification 2"
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

        notificationContext.AddNotifications(_notificationContextMessage);

        Assert.False(result.IsValid);
        Assert.True(notificationContext.HasNotifications());
    }
}


