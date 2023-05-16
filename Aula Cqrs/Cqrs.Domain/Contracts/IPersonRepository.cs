using System.Linq.Expressions;
using Cqrs.Domain.Domain;

namespace Cqrs.Domain.Contracts;
public interface IPersonRepository
{
    Task InsertAsync(
        Person person,
        CancellationToken cancellation);

    Task<IEnumerable<Person>> GetAsync(
        Expression<Func<Person, bool>> expression,
        CancellationToken cancellation);
}