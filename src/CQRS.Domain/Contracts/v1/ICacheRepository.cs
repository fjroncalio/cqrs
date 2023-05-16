namespace CQRS.Domain.Contracts.v1;

public interface ICacheRepository<T> where T : class
{
    Task<T?> GetAsync(string key);
    Task SetAsync(string key, T value);
}