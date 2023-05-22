using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;
using CQRS.Domain.Entities.v1;

namespace CQRS.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommandHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public CreatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var currentPerson = await _repository.FindByDocumentAsync(command.Cpf, cancellationToken);

        if (currentPerson is not null)
            //TODO add validation = $"There is already a person registered with this data."
            return Guid.Empty;

        var entity = _mapper.Map<Person>(command);
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}