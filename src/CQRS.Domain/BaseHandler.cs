using CQRS.Domain.Contracts.v1;

namespace CQRS.Domain;

public abstract class BaseHandler
{
    protected readonly INotificationContext NotificationContext;

    protected BaseHandler(INotificationContext notificationContext)
    {
        NotificationContext = notificationContext;
    }
}