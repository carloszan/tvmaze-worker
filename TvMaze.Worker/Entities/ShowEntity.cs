using MongoDB.Bson.Serialization.Attributes;

namespace TvMazeWorker.Entities
{
  public class Actor
  {
    [BsonElement("_id")]
    public int Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("Birthday")]
    public DateTime Birthday { get; set; }
  }

  public class ShowEntity
  {
    [BsonElement("_id")]
    public int Id { get;set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("cast")]
    public List<Actor> Cast { get; set; }
  }
}
