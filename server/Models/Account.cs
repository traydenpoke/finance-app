namespace MyPostgresApi.Models
{
  public class Account
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public float Balance { get; set; }
  }
}
