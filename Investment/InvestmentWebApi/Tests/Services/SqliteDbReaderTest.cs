using InvestmentWebApi.Services.Api;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Services;

/// <summary>
/// Tests the SqliteDbReader using an in memory Sqlite database.
/// </summary>
[TestFixture]
public class SqliteDbReaderTest {
  private SqliteDbReader _dbReader;
  private SqliteConnection _connection;

  private const string _createTables = @"
      CREATE TABLE IF NOT EXISTS users(
          user_id INTEGER NOT NULL PRIMARY KEY,
          name TEXT NOT NULL
      );

      CREATE TABLE IF NOT EXISTS stocks(
          stock_id INTEGER NOT NULL PRIMARY KEY,
          name TEXT NOT NULL
      );

      CREATE TABLE IF NOT EXISTS stock_prices (
          stock_id INTEGER NOT NULL,
          start_time TEXT NOT NULL,
          end_time TEXT NULL,
          price REAL NOT NULL,
          PRIMARY KEY(stock_id, start_time, end_time),
          FOREIGN KEY (stock_id)
              REFERENCES stocks (stock_id)
      );

      CREATE TABLE IF NOT EXISTS investments(
          investment_id INTEGER NOT NULL PRIMARY KEY,
          user_id INTEGER NOT NULL,
          stock_id INTEGER NOT NULL,
          share_count REAL NOT NULL,
          acquire_date TEXT NOT NULL,
          FOREIGN KEY (user_id)
              REFERENCES users (user_id),
          FOREIGN KEY (stock_id)
              REFERENCES stocks (stock_id)
      );
";


  private const string _fillTables = @"
      INSERT INTO users(user_id, name) VALUES (1, 'Mr. Mon O. Polly');

      INSERT INTO stocks(stock_id, name) VALUES (1, 'Pear'), (2, 'Coal'), (3, 'Lumber');

      INSERT INTO stock_prices(stock_id, start_time, end_time, price)
      VALUES
          (1, '2000-01-01T00:00:00', '2020-01-01T00:00:00', 50),
          (1, '2020-01-01T00:00:00', '2022-01-01T00:00:00', 70),
          (1, '2022-01-01T00:00:00', NULL, 90),
          (2, '2000-01-01T00:00:00', NULL, 150),
          (3, '2000-01-01T00:00:00', NULL, 290);

      INSERT INTO investments(investment_id, user_id, stock_id, share_count, acquire_date)
      VALUES
          (1, 1, 1, 20, '2011-05-06T00:00:00'),
          (3, 1, 3, 20, '2023-05-06T00:00:00');
";

  private void ExecuteCommand(string cmd) {
    using var command = _connection.CreateCommand();
    command.CommandText = cmd;
    command.ExecuteNonQuery();
  }

  [SetUp]
  public void Setup() {
    Mock<ILogger<SqliteDbReader>> mockLogger = new Mock<ILogger<SqliteDbReader>>();
    _connection = new SqliteConnection("Data Source=:memory:");
    _connection.Open();
    _dbReader = new SqliteDbReader(_connection, mockLogger.Object);
    ExecuteCommand(_createTables);
    ExecuteCommand(_fillTables);
  }

  [TearDown]
  public void TearDown() {
    _connection.Close();
  }

  [Test]
  public void GetInvestmentsForUserTest() {
    var investmentsForUser = _dbReader.GetInvestmentsForUser(1);

    Assert.That(investmentsForUser, Has.Count.EqualTo(2));
    Assert.That(investmentsForUser.Select(i=>i.StockId), Is.EquivalentTo(new int[] {1, 3}));
    Assert.That(investmentsForUser.Select(i=>i.StockName), Is.EquivalentTo(new string[] {"Pear", "Lumber"}));
  }
  
  [Test]
  public void GetInvestmentsForUserTest_NoMatch() {
    var investmentsForUser = _dbReader.GetInvestmentsForUser(8);

    Assert.That(investmentsForUser, Has.Count.EqualTo(0));
  }
  
  [Test]
  public void GetInvestmentDetailsTest_NoMatch() {
    var investmentDetails = _dbReader.GetInvestmentDetails(49);

    Assert.IsNull(investmentDetails);
  }
  
  [Test]
  public void GetInvestmentDetailsTest() {
    var investmentDetails = _dbReader.GetInvestmentDetails(3);

    Assert.IsNotNull(investmentDetails);
    Assert.That(investmentDetails!.StockId, Is.EqualTo(3));
    Assert.That(investmentDetails!.CostBasis, Is.EqualTo(290m));
    Assert.That(investmentDetails!.CurrentPrice, Is.EqualTo(290m));
    Assert.That(investmentDetails!.ShareCount, Is.EqualTo(20m));
    Assert.That(investmentDetails!.AcquireDate, Is.EqualTo(new DateTime(2023,5,6,0,0,0)));
    Assert.That(investmentDetails!.StockName, Is.EqualTo("Lumber"));
    Assert.That(investmentDetails!.UserId, Is.EqualTo(1));
    
          // (3, 1, 3, 20, '2023-05-06T00:00:00');
  }
}