
namespace MyPostgresApi.Utility
{

  public static class Helpers
  {
    public static bool IsInteger(string value)
    {
      return int.TryParse(value, out var result);
    }
  }
}