﻿using CQRS.Domain.Commands.CreatePerson;
using CQRS.Domain.Commands.DeletePerson;
using CQRS.Domain.Commands.UpdatePerson;
using CQRS.Domain.Contracts;
using CQRS.Domain.Domain;
using CQRS.Domain.Queries.GetPerson;
using CQRS.Domain.Queries.ListPerson;
using CQRS.Infra.Repository;
using CQRS.Infra.Repository.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MongoDB.Driver;

namespace CQRS;

public static class Bootstrap
{
    public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(configuration);
        services.AddMappers();
        services.AddCommands();
        services.AddQueries();
        services.AddValidators();

        return services;
    }

    private static void AddCommands(this IServiceCollection services)
    {
        services.AddTransient<CreatePersonCommandHandler>();
        services.AddTransient<DeletePersonCommandHandler>();
        services.AddTransient<UpdatePersonCommandHandler>();
    }

    private static void AddQueries(this IServiceCollection services)
    {
        services.AddTransient<ListPersonQueryHandler>();
        services.AddTransient<GetPersonQueryHandler>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddScoped<IValidator<UpdatePersonCommand>, UpdatePersonCommandValidator>();
    }

    private static void AddMappers(this IServiceCollection services) =>
        services.AddAutoMapper(
            typeof(CreatePersonCommandProfile),
            typeof(UpdatePersonCommandProfile),
            typeof(GetPersonQueryProfile),
            typeof(ListPersonQueryProfile));

    private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection(nameof(MongoRepositorySettings));
        var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.Get<MongoRepositorySettings>().ConnectionString);

        services.Configure<MongoRepositorySettings>(mongoSettings);
        services.AddSingleton<IMongoClient>(new MongoClient(clientSettings));
        services.AddSingleton<IPersonRepository, PersonRepository>();
    }
}