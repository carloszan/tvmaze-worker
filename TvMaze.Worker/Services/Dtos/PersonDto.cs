using Newtonsoft.Json;
using TvMazeWorker.Utils;

namespace TvMazeWorker.Services.Dtos
{
  public class PersonDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime? Birthday { get; set; }

  }
}
