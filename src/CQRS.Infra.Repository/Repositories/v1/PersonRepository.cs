using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Entities.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRS.Infra.Repository.Repositories.v1;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(IMongoClient client, IOptions<MongoRepositorySettings> settings)
        : base(client, settings)
    {
    }

    public async Task<Person?> FindByDocumentAsync(string? document, CancellationToken cancellationToken)
    {
        var filter = Builders<Person>.Filter.Eq(x => x.Cpf.Value, document.ToUpper());
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
}