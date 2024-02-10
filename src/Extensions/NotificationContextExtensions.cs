using HaidaiTech.Notificator.Interfaces;
using HaidaiTech.Notificator.NotificationContextMessages;
using HaidaiTech.Notificator.NotificationContextPattern;
using Microsoft.Extensions.DependencyInjection;

namespace HaidaiTech.Notificator.Extensions
{
    public static class NotificationContextExtensions
    {

        /// <summary>
        /// Registers a notification context service into the .NET Dependency Injection (DI) container with custom NotificationContextMessage
        /// </summary>     
        /// <typeparam name="TNotificationMessage">The custom notification context message</typeparam>
        /// <example>
        /// <code>
        /// services.AddNotificationContextService<MyNotificationMessage>();
        /// </code>
        /// </example>
        public static void AddNotificationContextService<TNotificationMessage>(this IServiceCollection services)
        where TNotificationMessage : class, INotificationContextMessage
        {
            services.AddScoped<INotificationContext<TNotificationMessage>, NotificationContext<TNotificationMessage>>();
        }


        /// <summary>
        /// Registers a notification context service into the .NET Dependency Injection (DI) container with generic NotificationContextMessage
        /// </summary>
        /// <example>
        /// <code>
        /// services.AddNotificationContextService();
        /// </code>
        /// </example>
        public static void AddNotificationContextService(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext<NotificationContextMessage>, NotificationContext<NotificationContextMessage>>();
        }
    }
}