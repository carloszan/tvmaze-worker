using MongoDB.Driver;
using TvMazeWorker.Entities;
using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeWorker.Repositories
{
  public class ShowRepository : IShowRepository
  {

    private const string databaseName = "shows";
    private const string collectionName = "shows";
    private readonly IMongoCollection<ShowEntity> _showCollection;

    public ShowRepository(IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      _showCollection = database.GetCollection<ShowEntity>(collectionName);
    }

    public async Task SaveShowAsync(List<ShowEntity> shows)
    {
      await _showCollection.InsertManyAsync(shows);
    }
  }
}
