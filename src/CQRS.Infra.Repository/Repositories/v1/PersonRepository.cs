using CQRS.Domain.Contracts.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using CQRS.Domain.Entities.v1;

namespace CQRS.Infra.Repository.Repositories.v1;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(IMongoClient client, IOptions<MongoRepositorySettings> settings)
        : base(client, settings) { }

    public async Task AddAsync(Person person, CancellationToken cancellation)
    {
        await Collection.InsertOneAsync(person, cancellationToken: cancellation);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellation)
    {
        await Collection.ReplaceOneAsync(
            entity => entity.Id!.Equals(person.Id), person,
            new ReplaceOptions { IsUpsert = true },
            cancellation);
    }

    public async Task RemoveAsync(Guid personId, CancellationToken cancellation)
    {
        var filter = Builders<Person>.Filter
            .Eq(restaurant => restaurant.Id, personId);

        await Collection.DeleteOneAsync(filter, cancellationToken: cancellation);
    }

    public async Task<Person> FindByIdAsync(Guid personId, CancellationToken cancellation)
    {
        var filter = Builders<Person>.Filter
            .Eq(restaurant => restaurant.Id, personId);

        return await Collection.Find(filter).FirstOrDefaultAsync(cancellation);
    }

    public async Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> expression, CancellationToken cancellation)
    {
        var filter = Builders<Person>.Filter.Where(expression);

        return await Collection.Find(filter).ToListAsync(cancellation);

    }
}