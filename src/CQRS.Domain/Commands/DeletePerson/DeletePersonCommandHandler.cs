using CQRS.Domain.Contracts;
using CQRS.Domain.Core;

namespace CQRS.Domain.Commands.DeletePerson;

public class DeletePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _repository;

    public DeletePersonCommandHandler(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeletePersonCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(command.Id, cancellationToken);
    }
}
