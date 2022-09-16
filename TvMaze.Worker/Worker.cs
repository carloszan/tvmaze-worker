using TvMazeWorker.TvMazeScraper;

namespace TvMazeWorker
{
  public class Worker : IHostedService
  {
    private readonly ILogger<Worker> _logger;
    private readonly ITvMazeScraperService _scraper;

    public Worker(ILogger<Worker> logger, ITvMazeScraperService scraper)
    {
      _logger = logger;
      _scraper = scraper;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      await DoTheWork();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    public async Task DoTheWork()
    {
      var page = 1;
      var shows = await _scraper.GetShowsAsync(page);

      Console.WriteLine(shows[0].Name);
      Console.WriteLine("finalized");
    }
  }
}