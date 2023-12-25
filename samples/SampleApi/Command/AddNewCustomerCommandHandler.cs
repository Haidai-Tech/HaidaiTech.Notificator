using MediatR;
using HaidaiTech.Notificator.Interfaces;
using HaidaiTech.Notificator.Helpers;
using HaidaiTech.Notificator.NotificationContextMessages;


namespace SampleApi.Command
{
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
}