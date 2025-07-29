using Npgsql;

namespace MyPostgresApi.Models
{
  public class Account : IDbModel
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public float Balance { get; set; }


    public void LoadFromReader(NpgsqlDataReader reader)
    {
      Id = reader.GetInt32(0);
      Description = reader.GetString(1);
      Type = reader.GetString(2);
      Balance = reader.GetFloat(3);
    }

    public (string Columns, string Parameters, List<NpgsqlParameter> Values) GetInsertDef()
    {
      var columns = "description, type, balance";
      var parameters = "@description, @type, @balance";

      var values = new List<NpgsqlParameter>
      {
        new("description", Description),
        new("type", Type),
        new("balance", Balance)
      };

      return (columns, parameters, values);
    }

    public void SetId(object id)
    {
      Id = Convert.ToInt32(id);
    }
  }
}
