using AutoMapper;
using CQRS.Domain.Contracts.v1;

namespace CQRS.Domain.Queries.Person.v1.Get;

public class GetPersonQueryHandler : BaseHandler
{
    private readonly ICacheRepository<Entities.v1.Person> _cacheRepository;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public GetPersonQueryHandler(
        INotificationContext notificationContext,
        IMapper mapper,
        IPersonRepository repository,
        ICacheRepository<Entities.v1.Person> cacheRepository) : base(notificationContext)
    {
        _mapper = mapper;
        _repository = repository;
        _cacheRepository = cacheRepository;
    }

    public async Task<GetPersonQueryResponse?> HandleAsync(GetPersonQuery command, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheRepository.GetAsync(command.Id.ToString());

        if (cachedEntity != null) return _mapper.Map<GetPersonQueryResponse>(cachedEntity);

        var dataBaseEntity = await _repository.GetByIdAsync(command.Id, cancellationToken);

        if (dataBaseEntity == null)
        {
            NotificationContext.AddNotification($"Person with id = {command.Id} does not exist.");
            return null;
        }

        //TODO set cache
        return _mapper.Map<GetPersonQueryResponse>(dataBaseEntity);
    }
}