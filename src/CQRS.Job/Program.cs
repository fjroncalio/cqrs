using CQRS.Job;
using CQRS.Job.Workers.v1;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var configuration = host.Configuration;

        services.AddRepositories(configuration);
        services.AddCache(configuration);
        services.AddHostedService<CacheLoaderWorker>();
    })
    .Build();

host.Run();