using TvMazeWorker.Entities;
using TvMazeWorker.Services.Dtos;
namespace TvMazeWorker.Services
{
  public interface ITvMazeScraperService
  {
    Task<List<Actor>> GetCastFromShowIdAsync(int id);
    Task<List<ShowDto>> GetShowsAsync(int page);
  }
}