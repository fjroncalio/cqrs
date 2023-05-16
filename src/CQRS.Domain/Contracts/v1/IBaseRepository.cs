using System.Linq.Expressions;

namespace CQRS.Domain.Contracts.v1;

public interface IBaseRepository<T> where T : IEntity
{
    Task InsertAsync(T entity, CancellationToken cancellation);

    Task UpdateAsync(T entity, CancellationToken cancellation);

    Task DeleteAsync(Guid id, CancellationToken cancellation);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation);

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation);
}