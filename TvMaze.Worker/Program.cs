using TvMazeWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddTransient<ITvMazeScraper, TvMazeScraper>();
      services.AddHostedService<Worker>();
    })
    .Build();

await host.StartAsync();
