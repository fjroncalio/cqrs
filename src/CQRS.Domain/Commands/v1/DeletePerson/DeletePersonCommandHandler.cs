using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;

namespace CQRS.Domain.Commands.v1.DeletePerson;

public class DeletePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _repository;

    public DeletePersonCommandHandler(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeletePersonCommand command, CancellationToken cancellationToken)
    {
        await _repository.RemoveAsync(command.Id, cancellationToken);
    }
}