using MyPostgresApi.Models;
using Npgsql;

namespace MyPostgresApi.Services
{
  public class AccountService
  {
    private readonly PostgresService _postgres;

    public AccountService(PostgresService postgres)
    {
      _postgres = postgres;
    }

    public async Task<List<Account>> GetAccountsAsync()
    {
      return await _postgres.GetTableAsync<Account>("accounts");
    }

    public async Task<Account?> GetAccountByIdAsync(string accountId)
    {
      return await _postgres.GetByIdAsync<Account>("accounts", accountId);
    }

    public async Task AddAccountAsync(Account account)
    {
      await _postgres.AddAsync(account, "accounts");
    }
  }
}
