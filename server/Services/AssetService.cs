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

    public async Task AddAssetAsync(Asset asset)
    {
      await _postgres.AddAsync(asset, "assets");
    }
  }
}
