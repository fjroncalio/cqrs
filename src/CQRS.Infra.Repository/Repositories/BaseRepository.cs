using System.Linq.Expressions;
using CQRS.Domain.Contracts.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRS.Infra.Repository.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
{
    protected readonly IMongoCollection<TEntity> Collection;

    protected BaseRepository(IMongoClient client, IOptions<MongoRepositorySettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellation)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: cancellation);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellation)
    {
        await Collection.FindOneAndReplaceAsync(entity => entity.Id!.Equals(entity.Id), entity,
            cancellationToken: cancellation);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellation)
    {
        var filter = Builders<TEntity>.Filter.Eq(restaurant => restaurant.Id, id);

        await Collection.DeleteOneAsync(filter, cancellation);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation)
    {
        var filter = Builders<TEntity>.Filter.Eq(restaurant => restaurant.Id, id);

        return await Collection.Find(filter).FirstOrDefaultAsync(cancellation);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellation)
    {
        var filter = Builders<TEntity>.Filter.Where(expression);

        return await Collection.Find(filter).ToListAsync(cancellation);
    }
}