using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Entities.v1;
using EasyNetQ;

namespace CQRS.Consumer.Handlers.v1;

public class UpdatePearsonConsumerHandler : BackgroundService
{
    private readonly IBus _bus;

    private readonly IPersonRepository _repository;

    public UpdatePearsonConsumerHandler(IPersonRepository repository, IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.SendReceive.ReceiveAsync<Person>("update-person-queue",
            async person => { await _repository.UpdateAsync(person, stoppingToken); }).ConfigureAwait(false);
    }
}