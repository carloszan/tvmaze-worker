using System.Net.Http.Json;
using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeWorker.TvMazeScraper
{
  public class TvMazeScraperService : ITvMazeScraperService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public TvMazeScraperService(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
    {
      _logger = logger;
      _httpClientFactory = httpClientFactory; 
    }

    public async Task<List<ShowDto>> GetShowsAsync(int page)
    {
      _logger.LogInformation("Getting shows from TvMaze...");

      var httpClient = _httpClientFactory.CreateClient();
      var shows = await httpClient.GetFromJsonAsync<List<ShowDto>>($"https://api.tvmaze.com/shows?page={page}");

      if (shows != null)
        return shows;
      return new List<ShowDto>();
    }
  }
}