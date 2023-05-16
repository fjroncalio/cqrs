using CQRS.Domain.Commands.Person.v1.Delete;
using CQRS.Domain.Contracts.v1;
using EasyNetQ;

namespace CQRS.Consumer.Handlers.v1;

public class DeletePersonConsumerHandler : BackgroundService
{
    private readonly IBus _bus;

    private readonly IPersonRepository _repository;

    public DeletePersonConsumerHandler(IPersonRepository repository, IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.SendReceive.ReceiveAsync<DeletePersonCommand>("delete-person-queue",
            async command => { await _repository.DeleteAsync(command.Id, stoppingToken); }).ConfigureAwait(false);
    }
}