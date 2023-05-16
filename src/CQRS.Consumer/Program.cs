using CQRS.Consumer;
using CQRS.Consumer.Handlers.v1;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var configuration = host.Configuration;

        services.AddRepositories(configuration);
        services.AddMessageBroker(configuration);

        services.AddHostedService<CreatePersonConsumerHandler>();
        services.AddHostedService<UpdatePearsonConsumerHandler>();
        services.AddHostedService<DeletePersonConsumerHandler>();
    })
    .Build();

host.Run();