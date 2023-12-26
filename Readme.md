# Haidai Notificator

Haidai Notificator is a project developed by the Haidai team, implementing the Notification Pattern as described by Martin Fowler [here](https://www.martinfowler.com/eaaDev/Notification.html).

## Release Versions

- **1.0.0:** The inaugural version of HaidaiTech.Notificator.
- **1.0.1:** Updated the .NET framework to version 6, addressing several bugs in the process.
- **1.1.1:** Correcting version format.

## Release Notes

### Version 1.2.1

This version introduces enhanced flexibility to the `NotificationContextMessage`. You can now define your preferred level of notification criticality. Implementation details can be found in the [tests](https://github.com/Haidai-Tech/notificator/tree/main/tests).

Two development helpers have been introduced:

- **NotificationContextErrorLevelHelper:** This class encompasses various error level codes. If additional Error Level Codes are necessary, feel free to extend the `NotificationContextErrorLevelHelper` (refer to the test project).
- **NotificatorErrorCodesHelper:** This class contains 100 Error Codes. For those requiring more Error Codes, it's possible to extend the `NotificatorErrorCodesHelper` (see the test project).

It's worth noting that during the use of FluentValidator, the code in the test project might appear inelegant when transferring data from FluentValidator to `HaidaiTech.Notificator`.

### Version 1.3.1

This version enhances flexibility in the `NotificationContext` class. You are now empowered to insert a single message or a list of messages, and the Notification Class will create a single `NotificationContextMessage` for each message. Implementation details are available in the [tests](https://github.com/Haidai-Tech/notificator/tree/main/tests), specifically in the methods `ShouldAddSingleMessageInNotificationContext`, `ShouldAddSingleMessageInNotificationContextAsync`, `ShouldAddListOfStringToNotificationContext`, and `ShouldAddListOfStringToNotificationContextAsync`.

### Version 2.0.0

This version further enhances flexibility in the `NotificationContext` class. You can now create your own notification context message or continue using `NotificationContextMessage`. This flexibility was recognized when using the notification context in a legacy project, where the Context Message had specific fields like `CreatedAt` and `Machine Name`. Another improvement is the removal of a dependency in `NotificationContextMessage`. It's now corrected.

We deemed it better to keep the library in .NET Standard 2.1 and keep it updated for .NET version 8.

## Demonstration

```C#
// All methods now need an implementation of INotificationContextMessage

var notificationContext = new NotificationContext<NotificationContextMessage>();

notificationContext.AddNotification(
    new NotificationContextMessage(
        "A normal notification",
        "errorLevel",
        "errorCode"
    )
);
```

In projects using a dependency injection container, it is necessary to register the `INotificationContext` interface by passing the `NotificationContextMessage` that you intend to use. See SamplesAPI in the samples directory.

```C#
// The library provides NotificationContextMessage. You can use it if you want.
// Register in your DI
builder.Services.AddScoped<INotificationContext<NotificationContextMessage>, NotificationContext<NotificationContextMessage>>();


//and use it !
_notificationContext.AddNotification(new NotificationContextMessage(
                    "xpto",
                    NotificationContextErrorLevelHelper.CRITICAL,
                    NotificatorErrorCodesHelper.ERROR_CODE_072
                ));

// Or you can create your own Notification Context message
// This class needs to implement `INotificationContextMessage`, and in the container, you must declare
public class MyOwnNotificationContextMessage : INotificationContextMessage
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
// and, Register in your DI
builder.Services.AddScoped<INotificationContext<MyOwnNotificationContextMessage>, NotificationContext<MyOwnNotificationContextMessage>>();

//and use it !
  
_notificationContext.AddNotification(new MyOwnNotificationContextMessage(
                DateTime.Now,
                "Machine 1",
                "Error Message",
                "myCustomErrorLevel",
                "myCustonErrorCode"
            ));

```
