using TvMazeWorker.Entities;
using TvMazeWorker.Repositories;
using TvMazeWorker.Services;

namespace TvMazeWorker
{
  public class Worker : IHostedService
  {
    private readonly ILogger<Worker> _logger;
    private readonly ITvMazeScraperService _scraper;
    private readonly IShowRepository _showRepository;

    public Worker(
      ILogger<Worker> logger, 
      ITvMazeScraperService scraper,
      IShowRepository showRepository)
    {
      _logger = logger;
      _scraper = scraper;
      _showRepository = showRepository;
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
      var page = await _showRepository.GetLastIdAsync();

      var showsDto = await _scraper.GetShowsAsync(page);

      var shows = showsDto
        .Select(dto => new ShowEntity { Id = dto.Id, Name = dto.Name })
        .ToList();

      await _showRepository.SaveAsync(shows);

      Console.WriteLine(shows[0].Name);
      Console.WriteLine("finalized");
    }
  }
}