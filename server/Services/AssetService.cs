using System.Collections.Generic;
using System.Threading.Tasks;
using MyPostgresApi.Models;
using Npgsql;
using MyPostgresApi.Utility;

namespace MyPostgresApi.Services
{
  public class AssetService
  {
    private readonly PostgresService _postgres;

    public AssetService(PostgresService postgres)
    {
      _postgres = postgres;
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
      var assetList = new List<Asset>();
      var sql = "SELECT * FROM assets";

      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      await using var reader = await cmd.ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
        assetList.Add(new Asset
        {
          Id = reader.GetInt32(0),
          AccountId = reader.GetInt32(1),
          Symbol = reader.GetString(2),
          Description = reader.GetString(3),
          Type = reader.GetString(4)
        });
      }

      return assetList;
    }

    public async Task<Asset> GetAssetByIdAsync(string assetId)
    {
      if (!Helpers.IsInteger(assetId)) return null;

      var sql = "SELECT * FROM assets WHERE id = @id";
      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("id", int.Parse(assetId));

      await using var reader = await cmd.ExecuteReaderAsync();
      if (await reader.ReadAsync())
      {
        return new Asset
        {
          Id = reader.GetInt32(0),
          AccountId = reader.GetInt32(1),
          Symbol = reader.GetString(2),
          Description = reader.GetString(3),
          Type = reader.GetString(4)
        };
      }

      return null;
    }

    public async Task<float?> GetAssetPriceByIdAsync(string assetId)
    {
      // check to see if asset exists
      var asset = await GetAssetByIdAsync(assetId);
      if (asset == null) return null;

      // get price via api
      return -1;
    }

    public async Task AddAssetAsync(Asset asset)
    {
      var sql = "INSERT INTO assets (account_id, symbol, description, type) VALUES (@account_id, @symbol, @description, @type) RETURNING id";

      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("account_id", asset.AccountId);
      cmd.Parameters.AddWithValue("symbol", asset.Symbol);
      cmd.Parameters.AddWithValue("description", asset.Description);
      cmd.Parameters.AddWithValue("type", asset.Type);

      // return new id
      var result = await cmd.ExecuteScalarAsync();
      asset.Id = Convert.ToInt32(result);
    }
  }
}
