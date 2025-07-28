using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace MyPostgresApi.Services
{
  public class PostgresService
  {
    private readonly string _connectionString;
    public string ConnectionString => _connectionString;

    public PostgresService(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("PostgresConnection");
    }

    private async Task CreateTable(string tableSql)
    {
      await using var conn = new NpgsqlConnection(_connectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(tableSql, conn);
      await cmd.ExecuteNonQueryAsync();
    }

    public async Task CreateAllTablesAsync()
    {
      await CreateTable(@"
        CREATE TABLE IF NOT EXISTS accounts (
          id SERIAL PRIMARY KEY,
          description TEXT NOT NULL UNIQUE,
          type TEXT CHECK(type IN ('cash', 'stock', 'crypto')) NOT NULL,
          balance NUMERIC NOT NULL
        );
      ");

      await CreateTable(@"
        CREATE TABLE IF NOT EXISTS assets (
          id SERIAL PRIMARY KEY,
          account_id INTEGER REFERENCES accounts(id),
          symbol TEXT NOT NULL,
          description TEXT NOT NULL,
          type TEXT CHECK(type IN ('stock', 'crypto')) NOT NULL
        );
      ");

      await CreateTable(@"
        CREATE TABLE IF NOT EXISTS transactions (
          id SERIAL PRIMARY KEY,
          account_id INTEGER NOT NULL REFERENCES accounts(id),
          asset_id INTEGER REFERENCES assets(id),
          type TEXT CHECK (type IN ('deposit', 'withdrawal', 'buy', 'sell')) NOT NULL,
          amount NUMERIC NOT NULL,
          value NUMERIC NOT NULL,
          currency TEXT CHECK (currency IN ('CAD', 'USD')) DEFAULT 'CAD',
          date TIMESTAMP NOT NULL,
          notes TEXT
        );
      ");
    }
  }
}
