using TvMazeWorker.Entities;

namespace TvMazeWorker.Repositories
{
  public interface IShowRepository
  {
    Task SaveShowAsync(List<ShowEntity> show);
  }
}
