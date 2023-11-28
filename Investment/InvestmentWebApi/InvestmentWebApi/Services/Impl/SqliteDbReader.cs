using System.Data;
using Microsoft.Data.Sqlite;

namespace InvestmentWebApi.Services.Api;

/// <summary>
/// Implementation of IDbReader using a Sqlite database.
/// </summary>
public class SqliteDbReader : IDbReader, IDisposable, IAsyncDisposable {
  private readonly SqliteConnection _connection;

  private readonly ILogger<SqliteDbReader> _logger;

  public SqliteDbReader(ILogger<SqliteDbReader> logger) : this("Data Source=investments.db", logger) { }

  public SqliteDbReader(string connectionString, ILogger<SqliteDbReader> logger) {
    _logger = logger;
    _connection = new SqliteConnection(connectionString);
    try {
      _connection.Open();
    }
    catch (SqliteException exception) {
      _logger.LogError(exception, "Exception while opening SqliteDbReader connection.");
    }
  }

  public SqliteDbReader(SqliteConnection connection, ILogger<SqliteDbReader> logger) {
    _connection = connection;
    _logger = logger;
  }

  public ICollection<InvestmentRecord> GetInvestmentsForUser(int userId) {
    using var command = _connection.CreateCommand();
    command.CommandText =
        @"
          SELECT
              i.investment_id,
              i.user_id,
              i.stock_id,
              s.name,
              i.share_count,
              i.acquire_date
          FROM investments i
          INNER JOIN stocks s
              on i.stock_id = s.stock_id
          where user_id = @userId;
      ";
    command.Parameters.AddWithValue("@userId", userId);

    List<InvestmentRecord> records = new List<InvestmentRecord>();

    try {
      using SqliteDataReader reader = command.ExecuteReader();
      while (reader.Read()) {
        InvestmentRecord record = new InvestmentRecord() {
            InvestmentId = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            StockId = reader.GetInt32(2),
            StockName = reader.GetString(3),
            ShareCount = reader.GetDecimal(4),
            AcquireDate = reader.GetDateTime(5)
        };
        records.Add(record);
      }
    }
    catch (SqliteException exception) {
      _logger.LogError(exception, "Exception while reading investments for user.");
    }

    return records;
  }

  public InvestmentDetailedRecord? GetInvestmentDetails(int investmentId) {
    using var command = _connection.CreateCommand();
    command.CommandText =
        @"
          SELECT
              i.investment_id,
              i.user_id,
              i.stock_id,
              s.name,
              i.share_count,
              i.acquire_date,
              cb.price as cost_basis,
              cp.price as current_price
          FROM investments i
          INNER JOIN stocks s
              on i.stock_id = s.stock_id
          LEFT JOIN stock_prices cb
              on cb.stock_id = i.stock_id
              and DATETIME(cb.start_time) <= DATETIME(i.acquire_date)
              and (cb.end_time is null
                   or DATETIME(cb.end_time > DATETIME(i.acquire_date)))
          LEFT JOIN stock_prices cp
              on cp.stock_id = i.stock_id
              and cp.end_time is null
          WHERE i.investment_id = @investmentId
          LIMIT 1;
      ";
    command.Parameters.AddWithValue("@investmentId", investmentId);

    try {
      using SqliteDataReader reader = command.ExecuteReader();
      while (reader.Read()) {
        InvestmentDetailedRecord record = new InvestmentDetailedRecord() {
            InvestmentId = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            StockId = reader.GetInt32(2),
            StockName = reader.GetString(3),
            ShareCount = reader.GetDecimal(4),
            AcquireDate = reader.GetDateTime(5),
            CostBasis = reader.GetDecimal(6),
            CurrentPrice = reader.GetDecimal(7)
        };
        return record;
      }
    }
    catch (SqliteException exception) {
      _logger.LogError(exception, "Exception while reading investments for user.");
    }

    return null;
  }

  public void Dispose() {
    _connection.Dispose();
  }

  public async ValueTask DisposeAsync() {
    await _connection.DisposeAsync();
  }
}