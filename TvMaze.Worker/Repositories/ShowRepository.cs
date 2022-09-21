using MongoDB.Bson;
using MongoDB.Driver;
using TvMazeWorker.Entities;

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

      var showList = shows.ToList();

      if (showList.Count > 0)
        return showList.LastOrDefault().Id;
      return -1;
    }

    public async Task InsertManyAsync(List<ShowEntity> shows)
    {
      await _showCollection.InsertManyAsync(shows, new InsertManyOptions() { IsOrdered = false});
    }

    public Task DeleteAllAsync()
    {
      return _showCollection.DeleteManyAsync(Builders<ShowEntity>.Filter.Empty);
    }

    public async Task<List<ShowEntity>> GetShowsWithoutCastAsync()
    {
      var filter = Builders<ShowEntity>.Filter.Eq("cast", BsonNull.Value);

      var shows = await _showCollection.FindAsync(filter, new FindOptions<ShowEntity, ShowEntity>()
      {
      });

      return shows.ToList();
    }

    public async Task UpdateAsync(ShowEntity show)
    {
      var filter = Builders<ShowEntity>.Filter.Eq("_id", show.Id);
      var updateDefinition = Builders<ShowEntity>.Update
        .Set(u => u.Cast, show.Cast);

      var res = await _showCollection.UpdateOneAsync(filter, updateDefinition);
    }
  }
}
