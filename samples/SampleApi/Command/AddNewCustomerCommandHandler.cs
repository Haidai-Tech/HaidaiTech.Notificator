using MediatR;
using Notificator.Interfaces;
using Notificator.NotificationContextPattern;
using Notificator.Helpers;

namespace SampleApi.Command
{
    public class AddNewCustomerCommandHandler
        : IRequestHandler<AddNewCustomerCommand, string>
    {

        private readonly INotificationContext _notificationContext;


        public AddNewCustomerCommandHandler(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task<string> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            //this is an example !!!!

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

            if (!_notificationContext.HasNotifications())
                return await Task.Run(() => { return "OK"; });

            return await Task.FromResult("Some errors needs you attention");

        }
    }
}