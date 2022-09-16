using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeWorker.TvMazeScraper
{
  public interface ITvMazeScraperService
  {
    Task<List<ShowDto>> GetShowsAsync(int page);
  }
}