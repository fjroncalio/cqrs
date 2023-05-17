using System.Net;
using AutoMapper;
using Cqrs.Domain.Contracts;
using Cqrs.Domain.Core;
using Cqrs.Domain.Domain;

namespace Cqrs.Domain.Commands.CreatePerson;
public class CreatePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(
        IPersonRepository personRepository,
        IMapper mapper
        )
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(
        CreatePersonCommand command,
        CancellationToken cancellationToken)
    {
        AddNotification("teste notification");
        SetStatusCode(HttpStatusCode.Ambiguous);

        var entity = _mapper.Map<Person>(command);

        await _personRepository.InsertAsync(entity, cancellationToken);

        return entity.Id;
    }
}