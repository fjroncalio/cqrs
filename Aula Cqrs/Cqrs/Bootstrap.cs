using Cqrs.Domain.Commands.CreatePerson;
using Cqrs.Domain.Contracts;
using Cqrs.Domain.Queries.ListPerson;
using Cqrs.Repository;
using Cqrs.Repository.Repositories;
using MongoDB.Driver;

namespace Cqrs.Api;
public static class Bootstrap
{
    public static void AddInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(configuration);
        services.AddCommands();
        services.AddQueries();
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddTransient<CreatePersonCommandHandler>();
    }

    private static void AddQueries(this IServiceCollection services)
    {
        services.AddTransient<ListPersonQueryHandler>();
    }
    
    private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
        var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.Get<MongoRepositorySettings>().ConnectionString);

        services.Configure<MongoRepositorySettings>(mongoSettings);
        services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
        services.AddSingleton<IPersonRepository, PersonRepository>();
    }
}