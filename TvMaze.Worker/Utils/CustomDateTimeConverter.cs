using Newtonsoft.Json.Converters;

namespace TvMazeWorker.Utils
{
  public class CustomDateTimeConverter : IsoDateTimeConverter
  {
    public CustomDateTimeConverter()
    {
      base.DateTimeFormat = "yyyy-MM-dd";
    }
  }
}
