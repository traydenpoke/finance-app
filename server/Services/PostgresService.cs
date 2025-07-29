using Npgsql;
using MyPostgresApi.Utility;

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

    // Generic return all rows of table
    public async Task<List<T>> GetTableAsync<T>(string tableName) where T : IDbModel, new()
    {
      var result = new List<T>();
      var sql = $"SELECT * from {tableName}";

      await using var conn = new NpgsqlConnection(_connectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      await using var reader = await cmd.ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
        var instance = new T();
        instance.LoadFromReader(reader);
        result.Add(instance);
      }

      return result;
    }

    // Generic return specific table row by id
    public async Task<T?> GetByIdAsync<T>(string tableName, string id) where T : IDbModel, new()
    {
      if (!Helpers.IsInteger(id)) return default;

      var sql = $"SELECT * from {tableName} WHERE id = @id";

      await using var conn = new NpgsqlConnection(_connectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("id", int.Parse(id));

      await using var reader = await cmd.ExecuteReaderAsync();

      if (await reader.ReadAsync())
      {
        var instance = new T();
        instance.LoadFromReader(reader);
        return instance;
      }

      return default;
    }

    // Generic insert
    public async Task AddAsync<T>(T model, string tableName) where T : IDbModel
    {
      var (columns, parameters, values) = model.GetInsertDef();
      var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters}) RETURNING id";

      await using var conn = new NpgsqlConnection(_connectionString);
      await conn.OpenAsync();

      await using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddRange(values.ToArray());

      var result = await cmd.ExecuteScalarAsync();
      model.SetId(result);
    }

  }
}
