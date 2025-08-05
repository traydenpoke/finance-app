using MyPostgresApi.Models;
using MyPostgresApi.Services;

namespace MyPostgresApi.Endpoints
{
  public static class AssetEndpoints
  {
    public static void MapAssetEndpoints(this WebApplication app)
    {
      // Get all assets
      app.MapGet("/assets", async (AssetService service) =>
      {
        var assetData = await service.GetAssetsAsync();
        return Results.Ok(assetData);
      })
      .WithName("GetAssets")
      .WithOpenApi();

      // Get asset by id
      app.MapGet("/assets/{id}", async (string id, AssetService service) =>
      {
        var assetData = await service.GetAssetByIdAsync(id);
        return Results.Ok(assetData);
      })
      .WithName("GetAssetById")
      .WithOpenApi();

      // Get asset price by id
      app.MapGet("/assets/{id}/price", async (string id, AssetService service) =>
      {
        var assetData = await service.GetAssetPriceByIdAsync(id);
        return Results.Ok(assetData);
      })
      .WithName("GetAssetPriceById")
      .WithOpenApi();

      // Get all asset prices
      app.MapGet("/assets/prices", async (AssetService service) =>
      {
        var priceData = await service.GetAssetPricesAsync();
        return Results.Ok(priceData);
      })
      .WithName("GetAssetPrices")
      .WithOpenApi();

      // Create an asset
      app.MapPost("/assets", async (Asset newAsset, AssetService service) =>
      {
        await service.AddAssetAsync(newAsset);
        return Results.Created($"/accounts/{newAsset.AccountId}/assets/{newAsset.Id}", newAsset);
      })
      .WithName("AddAsset")
      .WithOpenApi();
    }


  }
}
