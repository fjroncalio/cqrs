using System.Net;
using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;
using CQRS.Domain.Entities.v1;

namespace CQRS.Domain.Commands.v1.CreatePerson;

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
        AddNotification("teste");
        SetStatusCode(HttpStatusCode.ExpectationFailed);

        var entity = _mapper.Map<Person>(command);
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}