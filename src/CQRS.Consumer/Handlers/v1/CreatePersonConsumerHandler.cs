using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Entities.v1;
using EasyNetQ;

namespace CQRS.Consumer.Handlers.v1;

public class CreatePersonConsumerHandler : BackgroundService
{
    private readonly IBus _bus;

    private readonly IPersonRepository _repository;

    public CreatePersonConsumerHandler(IPersonRepository repository, IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.SendReceive.ReceiveAsync<Person>("create-person-queue",
            async person => { await _repository.InsertAsync(person, stoppingToken); }).ConfigureAwait(false);
    }
}