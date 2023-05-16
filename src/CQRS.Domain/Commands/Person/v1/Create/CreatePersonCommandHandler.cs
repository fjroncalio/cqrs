using AutoMapper;
using CQRS.Domain.Contracts.v1;
using EasyNetQ;

namespace CQRS.Domain.Commands.Person.v1.Create;

public class CreatePersonCommandHandler : BaseHandler
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(INotificationContext notificationContext, IMapper mapper, IBus bus) : base(
        notificationContext)
    {
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<Guid> HandleAsync(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.v1.Person>(command);

        await _bus.SendReceive.SendAsync("create-person-queue", entity, cancellationToken);

        return entity.Id;
    }
}