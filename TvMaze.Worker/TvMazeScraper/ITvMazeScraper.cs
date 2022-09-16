using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeWorker.TvMazeScraper
{
  public interface ITvMazeScraper
  {
    Task<List<ShowDto>> GetShows(int page);
  }
}