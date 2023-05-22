using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;
using System.Net;

namespace CQRS.Domain.Queries.v1.GetPerson;

public class GetPersonQueryHandler : BaseHandler
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetPersonQueryResponse?> HandleAsync(GetPersonQuery command, CancellationToken cancellationToken)
    {
        var dataBaseEntity = await _repository.FindByIdAsync(command.Id, cancellationToken);

        if (dataBaseEntity is not null)
            return _mapper.Map<GetPersonQueryResponse>(dataBaseEntity);

        AddNotification($"Person with id = {command.Id} does not exist.");
        SetStatusCode(HttpStatusCode.NotFound);
        return null;

    }
}