using AutoMapper;
using CQRS.Domain.Contracts;
using CQRS.Domain.Core;
using System.Net;

namespace CQRS.Domain.Queries.GetPerson;

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
        var dataBaseEntity = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (!string.IsNullOrWhiteSpace(dataBaseEntity?.Name))
            return _mapper.Map<GetPersonQueryResponse>(dataBaseEntity);

        AddNotification($"Person with id = {command.Id} does not exist.");
        SetStatusCode(HttpStatusCode.NotFound);
        return null;

    }
}