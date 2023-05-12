using AutoMapper;
using CQRS.Domain.Contracts;
using CQRS.Domain.Core;
using CQRS.Domain.Domain;

namespace CQRS.Domain.Commands.CreatePerson;

public class CreatePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Person>(command);
        await _repository.InsertAsync(entity, cancellationToken);
        return entity.Id;
    }
}