using TvMazeWorker.Entities;

namespace TvMazeWorker.Repositories
{
  public interface IShowRepository
  {
    Task SaveAsync(List<ShowEntity> show);
    Task<int> GetLastIdAsync();
  }
}
