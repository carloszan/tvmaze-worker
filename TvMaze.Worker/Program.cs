using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using TvMazeWorker;
using TvMazeWorker.Repositories;
using TvMazeWorker.TvMazeScraper;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
      services.AddOptions();
      BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

      services.AddTransient<ITvMazeScraperService, TvMazeScraperService>();
      services.AddSingleton<IMongoClient>(serviceProvider =>
      {
        IConfiguration configuration = hostContext.Configuration;
        var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        return new MongoClient(settings.ConnectionString);
      });

      services.AddSingleton<IShowRepository, ShowRepository>();

      services.AddHttpClient();
      services.AddHostedService<Worker>();
    })
    .Build();

await host.StartAsync();
