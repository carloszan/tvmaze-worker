using TvMazeWorker.Entities;

namespace TvMazeWorker.Repositories
{
  public interface IShowRepository
  {
    Task InsertManyAsync(List<ShowEntity> show);
    Task<int> GetLastIdAsync();
    Task DeleteAllAsync();
    Task<List<ShowEntity>> GetShowsWithoutCastAsync();
    Task UpdateAsync(ShowEntity show);
  }
}
