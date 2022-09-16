using MongoDB.Driver;
using Moq;
using TvMazeWorker.Repositories;

namespace TvMazeTests.Repositories
{
  public class ShowRepositoryTest
  {
    [Test]
    public async Task GetLastIdAsync_WithValidData_ReturnsLastId()
    {
      // Arrange
      var mongoClientStub = new Mock<MongoClient>();

      var showRepository = new ShowRepository(mongoClientStub.Object);
      var mockId = 1;

      // Act
      var id = await showRepository.GetLastIdAsync();

      // Assert
      Assert.Equals(id, mockId);
    }
  }
}
