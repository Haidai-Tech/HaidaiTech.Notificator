using System.Collections.Generic;
using HaidaiTech.Notificator.NotificationContextMessages;
using System.Linq;

#if !NETSTANDARD2_1
using System.ComponentModel.DataAnnotations;
#endif


namespace HaidaiTech.Notificator.Extensions
{
    public static partial class NotificationContextExtensions
    {
#if !NETSTANDARD2_1
        public IList<NotificationContextMessage> ToNotificationContext(IEnumerable<ValidationResult> validationResults)
        {
            var notificationContext = validationResults.Select(result =>
                new NotificationContextMessage(
                    message: result.ErrorMessage
                )).ToList();

            return notificationContext; 
        }
#endif
    }
}