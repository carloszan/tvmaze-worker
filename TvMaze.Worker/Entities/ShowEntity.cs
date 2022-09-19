namespace TvMazeWorker.Entities
{
  public class Cast
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
  }

  public class ShowEntity
  {
    public int Id { get;set; }
    public string Name { get; set; }
    public Cast? Cast { get; set; }
  }
}
