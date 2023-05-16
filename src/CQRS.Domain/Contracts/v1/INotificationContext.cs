using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Contracts.v1;

public interface INotificationContext
{
    IReadOnlyCollection<Notification> Notifications { get; }
    bool HasNotifications { get; }
    void AddNotification(string notification);
}