using CQRS.Domain.Domain;
using System.Linq.Expressions;

namespace CQRS.Domain.Contracts;
public interface IPersonRepository
{
    Task InsertAsync(Person person, CancellationToken cancellation);

    Task UpdateAsync(Person person, CancellationToken cancellation);

    Task DeleteAsync(Guid personId, CancellationToken cancellation);

    Task<Person> GetByIdAsync(Guid personId, CancellationToken cancellation);

    Task<IEnumerable<Person>> GetAsync(Expression<Func<Person, bool>> expression, CancellationToken cancellation);
}