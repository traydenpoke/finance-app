using MyPostgresApi.Models;
using Npgsql;
using MyPostgresApi.External;


namespace MyPostgresApi.Services
{
  public class AssetService
  {
    private readonly PostgresService _postgres;
    private readonly GoogleFinanceClient _google;

    public AssetService(PostgresService postgres, GoogleFinanceClient google)
    {
      _postgres = postgres;
      _google = google;
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
      return await _postgres.GetTableAsync<Asset>("assets");
    }

    public async Task<Asset?> GetAssetByIdAsync(string assetId)
    {
      return await _postgres.GetByIdAsync<Asset>("assets", assetId);
    }

    public async Task<float?> GetAssetPriceByIdAsync(string assetId)
    {
      // check to see if asset exists
      var asset = await GetAssetByIdAsync(assetId);
      if (asset == null) return null;

      var price = await _google.GetPriceAsync(asset.Symbol, asset.Type);
      return price;
    }

    public async Task<SortedDictionary<string, float>> GetAssetPricesAsync()
    {
      // Get all unique symbols
      var sql = "SELECT DISTINCT symbol, type FROM assets";

      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      await using var reader = await cmd.ExecuteReaderAsync();

      // Read one by one and get price for each one
      var prices = new SortedDictionary<string, float>();
      while (await reader.ReadAsync())
      {
        var symbol = reader.GetString(0);
        var type = reader.GetString(1);
        var price = await _google.GetPriceAsync(symbol, type);
        if (price == null) continue;

        prices.Add(symbol, (float)price);
      }

      return prices;
    }

    public async Task AddAssetAsync(Asset asset)
    {
      await _postgres.AddAsync(asset, "assets");
    }
  }
}
