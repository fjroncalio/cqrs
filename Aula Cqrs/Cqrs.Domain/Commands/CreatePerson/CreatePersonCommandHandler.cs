using Cqrs.Domain.Contracts;
using Cqrs.Domain.Domain;

namespace Cqrs.Domain.Commands.CreatePerson;
public class CreatePersonCommandHandler
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(
        IPersonRepository personRepository
        )
    {
        _personRepository = personRepository;
    }

    public async Task<Guid> HandleAsync(
        CreatePersonCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new Person
        {
            Name = command.Name,
            Cpf = command.Cpf,
            Email = command.Email,
            DateBirth = command.DateBirth
        };

        await _personRepository.InsertAsync(entity, cancellationToken);

        return entity.Id;
    }
}