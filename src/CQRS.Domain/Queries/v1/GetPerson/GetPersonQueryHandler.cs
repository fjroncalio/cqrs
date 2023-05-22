using AutoMapper;
using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Core.v1;

namespace CQRS.Domain.Queries.v1.GetPerson;

public class GetPersonQueryHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

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

        // TODO $"Person with id = {command.Id} does not exist."
        return null;
    }
}