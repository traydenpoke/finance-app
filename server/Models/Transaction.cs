namespace MyPostgresApi.Models
{
  public class Transaction
  {
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int? AssetId { get; set; }

    public string Type { get; set; } = null!;

    public float Amount { get; set; }

    public float Value { get; set; }

    public string Currency { get; set; } = "CAD";

    public DateTime Date { get; set; }

    public string? Notes { get; set; }
  }
}
