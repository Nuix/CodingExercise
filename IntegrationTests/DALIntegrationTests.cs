using InvestmentAPI;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace IntegrationTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DALIntegrationTests
    {
        ILogger _logger;
        string _connectionString;

        public DALIntegrationTests()
        {
            try
            {
                var loggerFactory = new LoggerFactory();
                _logger = loggerFactory.CreateLogger("IntegrationTests");
                Assert.IsNotNull(_logger, "Test setup failed to create logger");

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "localhost";
                //builder.UserID = "testUser";
                //builder.Password = "testUser";
                builder.IntegratedSecurity = true;
                builder.InitialCatalog = "InvestmentDB";
                builder.TrustServerCertificate = true;

                _connectionString = builder.ConnectionString;

            }
            catch (Exception ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }
        [TestMethod]
        public void Test_GetUserInvestments()
        {
            var userGuid = Guid.NewGuid();
            var investmentGuid1 = Guid.NewGuid();
            var investmentGuid2 = Guid.NewGuid();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TestUtilities testUtilities = new TestUtilities(connection);
                var insertResult = testUtilities.InsertUser(userGuid);
                Assert.IsNotNull(insertResult);

                var invest1Result = testUtilities.InsertInvestment(userGuid, investmentGuid1, "name1");
                Assert.IsNotNull(invest1Result);

                var invest2Result = testUtilities.InsertInvestment(userGuid, investmentGuid2, "name2");
                Assert.IsNotNull(invest2Result);

                var investmentDal = new InvestmentDAL(connection, _logger);
                var actual = investmentDal.GetInvestmentItemsByUserId(userGuid);

                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());
                Assert.AreEqual(investmentGuid1, actual[0].Id);
                Assert.AreEqual("name1", actual[0].Name);
                Assert.AreEqual(investmentGuid2, actual[1].Id);
                Assert.AreEqual("name2", actual[1].Name);

                testUtilities.DeleteInvestment(investmentGuid1);
                testUtilities.DeleteInvestment(investmentGuid2);
                testUtilities.DeleteUser(userGuid);
                testUtilities.Dispose();
            }
        }

        [TestMethod]
        public void Test_GetInvestmentRecordByInvestmentId()
        {
            var userGuid = Guid.NewGuid();
            var investmentGuid = Guid.NewGuid();
            var datePurchased = DateTime.UtcNow.AddMonths(-1);
            var costBasisPerShare = 10.34m;
            var currentPrice = 15.67m;
            long numberOfShares = 1000;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TestUtilities testUtilities = new TestUtilities(connection);
                var insertResult = testUtilities.InsertUser(userGuid);
                Assert.IsNotNull(insertResult);

                var investResult = testUtilities.InsertInvestment(userGuid, investmentGuid, "name1");
                Assert.IsNotNull(investResult);

                var recordResult = testUtilities.UpsertInvestmentRecord(investmentGuid, datePurchased,
                    costBasisPerShare, numberOfShares, currentPrice);
                Assert.IsNotNull(recordResult);


                var investmentDal = new InvestmentDAL(connection, _logger);
                var actual = investmentDal.GetInvestmentRecordByInvestmentId(investmentGuid);

                Assert.IsNotNull(actual);
                Assert.AreEqual(costBasisPerShare, actual.CostBasisPerShare);
                Assert.AreEqual(numberOfShares, actual.NumberOfShares);
                Assert.AreEqual(currentPrice, actual.CurrentPrice);
                Assert.AreEqual(Term.Short, actual.Term);
                Assert.AreEqual((numberOfShares * currentPrice) - (numberOfShares * costBasisPerShare), actual.TotalGainLoss);
                Assert.AreEqual(numberOfShares * currentPrice, actual.CurrentValue);

                testUtilities.DeleteInvestmentRecord(investmentGuid);
                testUtilities.DeleteInvestment(investmentGuid);
                testUtilities.DeleteUser(userGuid);
                testUtilities.Dispose();
            }
        }
    }
}