namespace TvMazeWorker
{
  public class Worker : IHostedService
  {
    private readonly ILogger<Worker> _logger;
    private readonly ITvMazeScraper _scraper;

    public Worker(ILogger<Worker> logger, ITvMazeScraper scraper)
    {
      _logger = logger;
      _scraper = scraper;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _scraper.GetShows(1);
      await Task.Delay(1000, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}