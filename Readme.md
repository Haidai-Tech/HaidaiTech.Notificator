# Haidai Notificator

Haidai Notificator is a project developed by the Haidai team, implementing the Notification Pattern as described by Martin Fowler [here](https://www.martinfowler.com/eaaDev/Notification.html).

## Release Versions

- **2.1.0:** Added Method Extension to inject NotificationContext to DI

## Release Notes

### Version 2.1.0

This release introduces an extension method that injects NotificationContext into the dependency injection container. Now you can do two things:

```csharp
// ASP.NET Core 

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

//Console Application

using System;
using Microsoft.Extensions.DependencyInjection;
using HaidaiTech.Notificator.Interfaces;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;
using HaidaiTech.Notificator.Extensions;

public class MyCustomNotificationContextMessage : INotificationContextMessage
{
    public string Message { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var notificationContext = serviceProvider.GetService<INotificationContext<MyCustomNotificationContextMessage>>();

        var notification = new MyCustomNotificationContextMessage { Message = "Nova notificação" };
        notificationContext.AddNotification(notification);

        if (notificationContext.HasNotifications())
        {
            Console.WriteLine("Existem notificações.");
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddNotificationContextService<MyCustomNotificationContextMessage>();
    }
}
```

```csharp

// Worker

 using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HaidaiTech.Notificator.Interfaces;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;
using HaidaiTech.Notificator.Extensions;

public class MyCustomNotificationContextMessage : INotificationContextMessage
{
    public string Message { get; set; }
}

public class Worker : BackgroundService
{
    private readonly INotificationContext<MyCustomNotificationContextMessage> _notificationContext;

    public Worker(INotificationContext<MyCustomNotificationContextMessage> notificationContext)
    {
        _notificationContext = notificationContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var notification = new MyCustomNotificationContextMessage { Message = "Nova notificação" };
            _notificationContext.AddNotification(notification);

            if (_notificationContext.HasNotifications())
            {
                Console.WriteLine("Existem notificações.");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddNotificationContextService<MyCustomNotificationContextMessage>();
                services.AddHostedService<Worker>();
            });
}
```

This project follow the TDD pattern. If you read the [tests](https://github.com/Haidai-Tech/HaidaiTech.Notificator/tree/main/tests), you will understand the using of NotificationContext class.
