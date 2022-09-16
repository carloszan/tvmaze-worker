using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using TvMazeWorker.TvMazeScraper;
using TvMazeWorker.TvMazeScraper.Dtos;

namespace TvMazeTests
{
  public class TvMazeTestsBase
  {
    public ITvMazeScraperService _service;

    [SetUp]
    public void Setup()
    {
    }
  }
}