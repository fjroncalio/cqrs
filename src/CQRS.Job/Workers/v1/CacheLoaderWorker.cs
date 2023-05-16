using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Entities.v1;

namespace CQRS.Job.Workers.v1;

public class CacheLoaderWorker : BackgroundService
{
    private readonly ILogger<CacheLoaderWorker> _logger;
    private readonly IPersonRepository _repository;
    private readonly ICacheRepository<Person> _cacheRepository;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public CacheLoaderWorker(ILogger<CacheLoaderWorker> logger, IPersonRepository repository, ICacheRepository<Person> cacheRepository, IHostApplicationLifetime applicationLifetime)
    {
        _logger = logger;
        _repository = repository;
        _cacheRepository = cacheRepository;
        _applicationLifetime = applicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start cache loader process from {entityName}", nameof(Person));

        var people = await _repository.GetAsync(x=> true, stoppingToken);
        
        foreach (var person in people)
        {
            var key = person.Id.ToString();

            _logger.LogInformation("Set cache key: {key}", key);

            await _cacheRepository.SetAsync(key, person);
        }

        _applicationLifetime.StopApplication();
    }
}