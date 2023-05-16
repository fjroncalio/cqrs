using CQRS.Domain.Contracts.v1;
using EasyNetQ;

namespace CQRS.Domain.Commands.Person.v1.Delete;

public class DeletePersonCommandHandler : BaseHandler
{
    private readonly IBus _bus;

    public DeletePersonCommandHandler(INotificationContext notificationContext, IBus bus) : base(notificationContext)
    {
        _bus = bus;
    }

    public async Task HandleAsync(DeletePersonCommand command, CancellationToken cancellationToken)
    {
        await _bus.SendReceive.SendAsync("delete-person-queue", command, cancellationToken);
    }
}