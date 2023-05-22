using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;
using CQRS.Domain.Entities.v1;
using CQRS.Domain.ValueObjects.v1;

namespace CQRS.Domain.Commands.v1.UpdatePerson;

public class UpdatePersonCommandHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public UpdatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var currentPerson = await _repository.FindByIdAsync(command.Id, cancellationToken);

        if (currentPerson is null)
            //TODO add validation = $"Person with id = {command.Id} does not exist."
            return Guid.Empty;

        var personUpdates = new Person(
            currentPerson.Id,
            new Name(command.Name),
            new Document(command.Cpf),
            new Email(command.Email),
            command.DateBirth,
            currentPerson.CreatedAt);

        await _repository.UpdateAsync(personUpdates, cancellationToken);
        return currentPerson.Id;
    }
}