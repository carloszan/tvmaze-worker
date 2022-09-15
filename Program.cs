using tvmaze_worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddHostedService<Worker>();
    })
    .Build();

await host.StartAsync();
