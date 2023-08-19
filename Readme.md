# Haidai Notificator

Haidai Notificator is a project developed by the Haidai team, implementing the Notification Pattern as described by Martin Fowler [here](https://www.martinfowler.com/eaaDev/Notification.html).

## Release Versions

- **1.0.0:** The inaugural version of Notificator.
- **1.0.1:** Updated the .NET framework to version 6, addressing several bugs in the process.

## Release Notes

### Version 1.1.1

In this version, we introduce enhanced flexibility to the NotificationContextMessage. You are now empowered to define your preferred level of notification criticality. Refer to the tests for implementation details: [tests](https://github.com/Haidai-Tech/notificator/tree/main/tests).

Two development helpers have been introduced:

- **NotificationContextErrorLevelHelper:** This class encompasses various error level codes. Should additional Error Level Codes be necessary, feel free to extend the NotificationContextErrorLevelHelper (refer to the test project).
- **NotificatorErrorCodesHelper:** This class contains 100 Error Codes. For those requiring more Error Codes, it's possible to extend the NotificatorErrorCodesHelper (see the test project).

Furthermore, it's worth noting that during the use of FluentValidator, the code in the test project might appear inelegant when transferring data from FluentValidator to Notificator.

This revised version maintains your intended information while presenting it in a more organized and easy-to-follow structure.