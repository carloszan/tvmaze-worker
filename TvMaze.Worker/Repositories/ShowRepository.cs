using MongoDB.Bson;
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

    public async Task<int> GetLastIdAsync()
    {
      var filter = Builders<ShowEntity>.Filter.Empty;
      var sort = Builders<ShowEntity>.Sort.Ascending("_id");

      var shows = await _showCollection.FindAsync(filter, new FindOptions<ShowEntity, ShowEntity>()
      {
        Sort = sort
      });

      var showList = await shows.ToListAsync();
      
      if (showList.Count > 0)
        return showList.LastOrDefault().Id;
      return -1;
    }

    public async Task SaveAsync(List<ShowEntity> shows)
    {
      await _showCollection.InsertManyAsync(shows);
    }
  }
}
