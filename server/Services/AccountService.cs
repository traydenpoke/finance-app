using System.Collections.Generic;
using System.Threading.Tasks;
using MyPostgresApi.Models;
using Npgsql;
using MyPostgresApi.Utility;

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
      var accountList = new List<Account>();
      var sql = "SELECT * FROM accounts";

      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      await using var reader = await cmd.ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
        accountList.Add(new Account
        {
          Id = reader.GetInt32(0),
          Description = reader.GetString(1),
          Type = reader.GetString(2),
          Balance = reader.GetFloat(3)
        });
      }

      return accountList;
    }

    public async Task<Account> GetAccountByIdAsync(string accountId)
    {
      if (!Helpers.IsInteger(accountId)) return null;

      var sql = "SELECT * FROM accounts WHERE id = @id";
      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("id", int.Parse(accountId));

      await using var reader = await cmd.ExecuteReaderAsync();
      if (await reader.ReadAsync())
      {
        return new Account
        {
          Id = reader.GetInt32(0),
          Description = reader.GetString(1),
          Type = reader.GetString(2),
          Balance = reader.GetFloat(3)
        };
      }

      return null;
    }

    public async Task AddAccountAsync(Account account)
    {
      var sql = "INSERT INTO accounts (description, type, balance) VALUES (@description, @type, @balance) RETURNING id";

      await using var conn = new NpgsqlConnection(_postgres.ConnectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("description", account.Description);
      cmd.Parameters.AddWithValue("type", account.Type);
      cmd.Parameters.AddWithValue("balance", account.Balance);

      // return new id
      var result = await cmd.ExecuteScalarAsync();
      account.Id = Convert.ToInt32(result);
    }
  }
}
