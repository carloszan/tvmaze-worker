using System.Net.Http.Json;
using TvMazeWorker.Services.Dtos;

namespace TvMazeWorker.Services
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

    public async Task<List<CastDto>> GetCastFromShowIdAsync(int showId)
    {
      _logger.LogInformation("Getting cast from TvMaze...");

      var httpClient = _httpClientFactory.CreateClient();
      try
      {
        var cast = await httpClient.GetFromJsonAsync<List<CastDto>>($"https://api.tvmaze.com/shows/{showId}/cast");

        if (cast != null)
          return cast;
      }
      catch (Exception)
      {
        return null;
      }
      return null;
    }

    public async Task<List<ShowDto>> GetShowsAsync(int page)
    {
      _logger.LogInformation("Getting shows from TvMaze...");

      var httpClient = _httpClientFactory.CreateClient();
      try
      {
        var shows = await httpClient.GetFromJsonAsync<List<ShowDto>>($"https://api.tvmaze.com/shows?page={page}");

        if (shows != null)
          return shows;
      }
      catch(Exception)
      {
        return null;
      }
      return null;
    }
  }
}