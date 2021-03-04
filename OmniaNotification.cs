using Radzen;

namespace omnia_blazor_demo
{

    public class OmniaNotification
    {
        public readonly NotificationService notificationService;

        public OmniaNotification(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void SendSuccess(string message)
        {
            notificationService.Notify(new NotificationMessage
            {
                 Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = message,
                Duration = 4000
            });
        }

        public void SendError(string message)
        {
            notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = message,
                Duration = 10000
            });
        }
    }
}
