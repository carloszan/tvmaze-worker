using System.Threading;
using TvMazeWorker.Entities;
using TvMazeWorker.Repositories;
using TvMazeWorker.Services;
using TvMazeWorker.Services.Dtos;

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
      await DoTheWork(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    public async Task DoTheWork(CancellationToken cancellationToken)
    {
      // Development purposes:
      await _showRepository.DeleteAllAsync();

      var lastId = await _showRepository.GetLastIdAsync();

      var page = Math.Floor(Convert.ToDouble(lastId) / 250) + 1;

      await FetchShowsAndSaveThem((int)page, cancellationToken);

      await FetchCastsAndSaveThem(cancellationToken);

      _logger.LogInformation("Finalized...");
    }

    private async Task FetchShowsAndSaveThem(int page, CancellationToken cancellationToken)
    {
      var showsDto = await _scraper.GetShowsAsync(page);

      do
      {
        if (cancellationToken.IsCancellationRequested)
        {
          break;
        }

        _logger.LogInformation("Saving shows to its document...");
        var shows = showsDto
          .Select(dto => new ShowEntity { Id = dto.Id, Name = dto.Name })
          .ToList();
        await _showRepository.InsertManyAsync(shows);

        // Each requests must wait 500 to send it again.
        // This is import as TvMazeApi has a rate limiting.
        Thread.Sleep(500);

        page = page + 1;
        showsDto = await _scraper.GetShowsAsync(page);
      } while (showsDto != null);
    }

    private async Task FetchCastsAndSaveThem(CancellationToken cancellationToken)
    {
    var showsWithoutCast = await _showRepository.GetShowsWithoutCastAsync();

      foreach (var showWithouCast in showsWithoutCast)
      {
        if (cancellationToken.IsCancellationRequested)
          break;

        // Each requests must wait 500 to send it again.
        // This is import as TvMazeApi has a rate limiting.
        Thread.Sleep(500);

        var cast = await _scraper.GetCastFromShowIdAsync(showWithouCast.Id);

        var sortedCast = cast
          .OrderByDescending(cast => cast.Person.Birthday)
          .Select(cast => new Actor
          {
            Id = cast.Person.Id,
            Name = cast.Person.Name,
            Birthday = cast.Person.Birthday,
          })
          .ToList();

        showWithouCast.Cast = sortedCast;

        _logger.LogInformation("Saving cast to its document...");
        await _showRepository.UpdateAsync(showWithouCast);
      }
    }
  }
}