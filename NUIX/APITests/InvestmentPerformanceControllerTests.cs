using API.Controllers;
using Application.ConcreteObjects;
using Application.Core;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Persistence;
using Persistence.Repositories;
using System.Net;

namespace APITests
{
    [TestFixture]
    public class InvestmentPerformanceControllerTests
    {
        private const int USER_ID_TEST = 1;
        private const int INVESTMENT_ID_TEST = 1;
        private ILogger<InvestmentPerformanceController> _logger = new Mock<ILogger<InvestmentPerformanceController>>().Object;
        [Test]
        public async Task GetInvestmentsListByUserIdTest()
        {
            var context = await GetDataBaseContext();
            var controller = new InvestmentPerformanceController(GetService(context), _logger);
            var res = controller.GetInvestmentslist(userId: USER_ID_TEST);
            var statusCodeResult = res.Result as ObjectResult;
            
            Assert.IsNotNull(statusCodeResult);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public async Task GetStockInvestmentDetailsTest()
        {
            var context = await GetDataBaseContext();
            var controller = new InvestmentPerformanceController(GetService(context), _logger);
            var res = controller.GetInvestmentDetails(investmentId: INVESTMENT_ID_TEST);
            var statusCodeResult = res.Result as ObjectResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        /// <summary>
        /// DataBase context for testing purposes. We cannot use the context for a real database since we need to make sure our tests
        /// are not gonna change the actual data.
        /// </summary>
        /// <returns>A DataBase context with fake data loaded in memory</returns>
        private static async Task<DataBaseContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var databaseContext = new DataBaseContext(options);
            databaseContext.Database.EnsureCreated();

            await Seed.SeedData(databaseContext);
            return databaseContext;
        }

        private InvestmentPerformanceService GetService(DataBaseContext context)
        {
            var service = new InvestmentPerformanceService(
               new Mock<InvestmentRepository>(context).Object,
               new Mock<InvestmentDetailFactory>(
                                        GetServiceProvider(context)
                                        
                   ).Object
               );
            return service;
        }

        private IServiceProvider GetServiceProvider(DataBaseContext context)
        {
            var serviceCollection = new ServiceCollection(); 
            serviceCollection.AddScoped(sp => new Mock<StockDetail>(
                    new Mock<StockInvestmentRepository>(context).Object,
                    new Mock<StockRepository>(context).Object
                ).Object);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
