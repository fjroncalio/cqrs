using AutoMapper;
using CQRS.Domain.Contracts.v1;
using EasyNetQ;

namespace CQRS.Domain.Commands.Person.v1.Update;

public class UpdatePersonCommandHandler : BaseHandler
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public UpdatePersonCommandHandler(INotificationContext notificationContext, IMapper mapper,
        IPersonRepository repository, IBus bus) : base(notificationContext)
    {
        _mapper = mapper;
        _repository = repository;
        _bus = bus;
    }

    public async Task HandleAsync(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var dataBaseEntity = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (dataBaseEntity is null)
        {
            NotificationContext.AddNotification($"Person with id = {command.Id} does not exist.");
            return;
        }

        var entity = _mapper.Map<Entities.v1.Person>(command);
        await _bus.SendReceive.SendAsync("update-person-queue", entity, cancellationToken);
    }
}