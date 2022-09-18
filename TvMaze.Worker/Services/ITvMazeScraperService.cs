using TvMazeWorker.Services.Dtos;

namespace TvMazeWorker.Services
{
  public interface ITvMazeScraperService
  {
    Task<List<ShowDto>> GetShowsAsync(int page);
  }
}