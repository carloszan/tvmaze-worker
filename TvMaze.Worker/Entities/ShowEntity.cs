namespace TvMazeWorker.Entities
{
  public class Actor
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
  }

  public class ShowEntity
  {
    public int Id { get;set; }
    public string Name { get; set; }
    public List<Actor> Cast { get; set; }
  }
}
