using MyPostgresApi.Models;
using MyPostgresApi.Services;

namespace MyPostgresApi.Endpoints
{
  public static class AccountEndpoints
  {
    public static void MapAccountEndpoints(this WebApplication app)
    {
      // Get all accounts
      app.MapGet("/accounts", async (AccountService service) =>
      {
        var accountData = await service.GetAccountsAsync();
        return Results.Ok(accountData);
      })
      .WithName("GetAccounts")
      .WithOpenApi();

      // Get account by id
      // Get all accounts
      app.MapGet("/accounts/{id}", async (string id, AccountService service) =>
      {
        var accountData = await service.GetAccountByIdAsync(id);
        return Results.Ok(accountData);
      })
      .WithName("GetAccountById")
      .WithOpenApi();

      // Create an account
      app.MapPost("/accounts", async (Account newAccount, AccountService service) =>
      {
        await service.AddAccountAsync(newAccount);
        return Results.Created($"/accounts/{newAccount.Id}", newAccount);
      })
      .WithName("AddAccount")
      .WithOpenApi();
    }
  }
}
