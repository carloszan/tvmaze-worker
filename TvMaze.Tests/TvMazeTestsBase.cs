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

    [Test]
    public async Task TestMazeScraper_WithValidPage_ReturnsShows()
    {
      // Arrange
      var mockData = new List<ShowDto>() { new ShowDto() { Id = 1, Name = "Game of Thrones" } };

      HttpResponseMessage resultMock = new HttpResponseMessage
      {
        StatusCode = System.Net.HttpStatusCode.OK,
        Content = new StringContent(JsonConvert.SerializeObject(mockData))
      };

      var loggerStub = new Mock<ILogger<Worker>>();
      var httpClientFactoryStub = new Mock<IHttpClientFactory>();

      var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

      mockHttpMessageHandler
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
          )
          .ReturnsAsync(resultMock)
          .Verifiable();

      var client = new HttpClient(mockHttpMessageHandler.Object);
      httpClientFactoryStub
        .Setup(_ => _.CreateClient(It.IsAny<string>()))
        .Returns(client);

      _service = new TvMazeScraperService(loggerStub.Object, httpClientFactoryStub.Object);

      // Act
      var shows = await _service.GetShowsAsync(1);
      // Assert
      Assert.IsNotEmpty(shows);
    }
  }
}