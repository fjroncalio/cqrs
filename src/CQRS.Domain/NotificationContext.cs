using CQRS.Domain.Contracts.v1;
using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain;

public class NotificationContext : INotificationContext
{
    private readonly List<Notification> _notifications;

    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Any();

    public void AddNotification(string notification)
    {
        _notifications.Add(new Notification(notification));
    }
}