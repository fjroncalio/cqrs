using System.Text.Json;
using CQRS.Domain.Contracts.v1;
using Microsoft.Extensions.Caching.Distributed;

namespace CQRS.Infra.Cache.Repositories.v1;

public class CacheRepository<T> : ICacheRepository<T> where T : class
{
    private readonly IDistributedCache _cache;
    private readonly CacheRepositorySettings _settings;

    public CacheRepository(IDistributedCache cache, CacheRepositorySettings settings)
    {
        _cache = cache;
        _settings = settings;
    }

    public async Task<T?> GetAsync(string key)
    {
        var cachedUser = await _cache.GetStringAsync(key);

        return !string.IsNullOrEmpty(cachedUser) ? JsonSerializer.Deserialize<T?>(cachedUser) : null;
    }

    public async Task SetAsync(string key, T value)
    {
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_settings.TimeToLiveInSeconds)
        });
    }
}