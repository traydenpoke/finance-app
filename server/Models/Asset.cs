namespace MyPostgresApi.Models
{
  public class Asset
  {
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
  }
}
