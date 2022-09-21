using Microsoft.Extensions.Logging;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using TvMazeWorker.Services.Dtos;
using TvMazeWorker.Services;
using TvMazeWorker.Entities;

namespace TvMazeTests.Services
{
  public class TvMazeScraperServiceTest
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TvMazeScraper_WithValidPage_ReturnsShows()
    {
      var mockData = new List<ShowDto>() { new ShowDto() { Id = 1, Name = "Game of Thrones" } };

      var loggerStub = new Mock<ILogger<Worker>>();

      var content = new StringContent(JsonConvert.SerializeObject(mockData));
      Mock<IHttpClientFactory> httpClientFactoryStub = HttpClientFactory(content);

      var service = new TvMazeScraperService(loggerStub.Object, httpClientFactoryStub.Object);

      // Act
      var shows = await service.GetShowsAsync(1);
      // Assert
      Assert.IsNotEmpty(shows);
    }

    [Test]
    public async Task TvMazeScraper_WithInvalidPage_ReturnsNull()
    {
      HttpResponseMessage resultMock = new HttpResponseMessage
      {
        StatusCode = System.Net.HttpStatusCode.NotFound,
      };

      var loggerStub = new Mock<ILogger<Worker>>();

      Mock<IHttpClientFactory> httpClientFactoryStub = NotFoundHttpClientFactory();

      var service = new TvMazeScraperService(loggerStub.Object, httpClientFactoryStub.Object);

      // Act
      var shows = await service.GetShowsAsync(1);
      // Assert
      Assert.IsNull(shows);
    }

    [Test]
    public async Task GetCastFromShowIdAsync_WithValidData_ReturnsCast()
    {
      // Arrange
      var id = 1;
      var name = "John Travolta";
      var mockData = new List<CastDto>() { 
        new CastDto 
          { 
           Person = new PersonDto()
           {
            Id = id, 
            Name = name, 
            Birthday = new DateTime(1954, 2, 18) 
           }
        } 
      };
      var loggerStub = new Mock<ILogger<Worker>>();

      var content = new StringContent(JsonConvert.SerializeObject(mockData));
      Mock<IHttpClientFactory> httpClientFactoryStub = HttpClientFactory(content);

      var service = new TvMazeScraperService(loggerStub.Object, httpClientFactoryStub.Object);

      // Act
      var cast = await service.GetCastFromShowIdAsync(1);
      // Assert
      Assert.That(cast.FirstOrDefault().Person.Id, Is.EqualTo(id));
      Assert.That(cast.FirstOrDefault().Person.Name, Is.EqualTo(name));
    }

    [Test]
    public async Task GetCastFromShowIdAsync_WithWrongId_ReturnsNull()
    {
      // Arrange
      var id = -1;
      var name = "John Travolta";
      List<Actor> mockData = null;
      var loggerStub = new Mock<ILogger<Worker>>();

      var content = new StringContent(JsonConvert.SerializeObject(mockData));
      Mock<IHttpClientFactory> httpClientFactoryStub = HttpClientFactory(content);

      var service = new TvMazeScraperService(loggerStub.Object, httpClientFactoryStub.Object);

      // Act
      var cast = await service.GetCastFromShowIdAsync(1);
      // Assert
      Assert.That(cast, Is.Null);
    }

    private static Mock<IHttpClientFactory> HttpClientFactory(StringContent content)
    {
      HttpResponseMessage resultMock = new HttpResponseMessage
      {
        StatusCode = System.Net.HttpStatusCode.OK,
        Content = content
      };

      return AbstractHttpClientFactory(resultMock);
    }

    private static Mock<IHttpClientFactory> NotFoundHttpClientFactory()
    {
      HttpResponseMessage resultMock = new HttpResponseMessage
      {
        StatusCode = System.Net.HttpStatusCode.NotFound,
      };

      return AbstractHttpClientFactory(resultMock);
    }

    private static Mock<IHttpClientFactory> AbstractHttpClientFactory(HttpResponseMessage resultMock)
    {
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
      return httpClientFactoryStub;
    }
  }
}