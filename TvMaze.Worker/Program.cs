using TvMazeWorker;
using TvMazeWorker.TvMazeScraper;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
      services.AddHttpClient();
      services.AddTransient<ITvMazeScraper, TvMazeScraper>();
      services.AddHostedService<Worker>();
    })
    .Build();

await host.StartAsync();
