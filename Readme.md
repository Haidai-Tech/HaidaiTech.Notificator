# Haidai Notificator

Haidai Notificator is a project developed by the Haidai team, implementing the Notification Pattern as described by Martin Fowler [here](https://www.martinfowler.com/eaaDev/Notification.html).

## Release Versions

- **2.1.0:** Added Method Extension to inject NotificationContext to DI

## Release Notes

### Version 2.1.0

This release introduces an extension method that injects NotificationContext into the dependency injection container. Now you can do two things:

```csharp
// Program.cs

using HaidaiTech.Notificator.Interfaces;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;
using HaidaiTech.Notificator.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddNotificationContextService();

//using your own notification context message
builder.Services.AddNotificationContextService<MyCustomNotificationContextMessage>();

//if you want to use your own DI, use this

builder.Services.AddScoped<INotificationContext<NotificationContextMessage>, NotificationContext<NotificationContextMessage>>();

//or 
builder.Services.AddScoped<INotificationContext<MyCustomNotificationContextMessage>, NotificationContext<MyCustomNotificationContextMessage>>();

```


```csharp
// Example using DDD pattern

 public class AddNewCustomerCommandHandler
        : IRequestHandler<AddNewCustomerCommand, string>
    {

        private readonly INotificationContext<NotificationContextMessage> _notificationContext;


        public AddNewCustomerCommandHandler(INotificationContext<NotificationContextMessage> notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task<string> Handle(
            AddNewCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            //this code is only AN EXAMPLE, read again : E-X-A-M-P-L-E !!!! XD~

            if (request.Name is null)
                _notificationContext.AddNotification(new NotificationContextMessage(
                    "The name is necessary",
                    NotificationContextErrorLevelHelper.ATTENTION,
                    NotificatorErrorCodesHelper.ERROR_CODE_027
                ));

            if (request.Age < 18)
                _notificationContext.AddNotification(new NotificationContextMessage(
                    "You must be 18",
                    NotificationContextErrorLevelHelper.CRITICAL,
                    NotificatorErrorCodesHelper.ERROR_CODE_072
                ));


            if (_notificationContext.HasNotifications())
                return await Task.Run(() =>
                {
                    return "Problems";
                });
            else
                return await Task.Run(() =>
                {
                    return "Ok";
                });

        }
    }

```

```csharp
 //on the controller

 [Route("[controller]")]
    public class CustomerController : Controller
    {

        private readonly IMediator _mediator;
        private readonly INotificationContext<NotificationContextMessage> _notificationContext;

        public CustomerController(
            IMediator mediator,
            INotificationContext<NotificationContextMessage> notificationContext
        )
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddNewCustomerCommand command)
        {
            var response = await _mediator.Send(command);

            if (_notificationContext.HasNotifications())
                return BadRequest(_notificationContext.GetNotifications());

            return Ok(response);
        }
    }

```


This project follow the TDD pattern. If you read the [tests](https://github.com/Haidai-Tech/HaidaiTech.Notificator/tree/main/tests), you will understand the using of NotificationContext class.
