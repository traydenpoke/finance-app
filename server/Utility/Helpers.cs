
namespace MyPostgresApi.Utility
{

  public static class Helpers
  {
    public static Boolean IsInteger(string value)
    {
      return int.TryParse(value, out var result);
    }
  }
}