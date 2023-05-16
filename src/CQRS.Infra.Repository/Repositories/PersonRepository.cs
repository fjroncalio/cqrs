using CQRS.Domain.Contracts.v1;
using CQRS.Domain.Entities.v1;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CQRS.Infra.Repository.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(IMongoClient client, IOptions<MongoRepositorySettings> settings)
        : base(client, settings)
    {
    }
}