using System.Net.Http.Json;
using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeWorker.TvMazeScraper
{
  public class TvMazeScraper : ITvMazeScraper
  {
    private readonly IHttpClientFactory _httpClientFactory;
    public TvMazeScraper(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory; 
    }

    public async Task<List<ShowDto>> GetShows(int page)
    {
      Console.WriteLine("getting shows from tvmaze...");
      var httpClient = _httpClientFactory.CreateClient();
      var shows = await httpClient.GetFromJsonAsync<List<ShowDto>>($"https://api.tvmaze.com/shows?page={page}");

      if (shows != null)
        return shows;
      return new List<ShowDto>();
    }
  }
}