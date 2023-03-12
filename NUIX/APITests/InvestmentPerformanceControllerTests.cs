using API.Controllers;
using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence;
using Persistence.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests
{
    [TestFixture]
    public class InvestmentPerformanceControllerTests
    {
        [Test]
        public async Task GetInvestmentsListByUserIdTest()
        {
            var context = await GetDataBaseContext();
            var controller = new InvestmentPerformanceController(GetService(context));
            var res = controller.GetInvestmentslist(userId: 1);

            Assert.That(res.Value, Is.Not.Null);
        }

        [Test]
        public async Task GetStockInvestmentDetailsTest()
        {
            var context = await GetDataBaseContext();
            var controller = new InvestmentPerformanceController(GetService(context));
            var res = controller.GetInvestmentDetails(investmentId: 1);

            Assert.That(res.Value, Is.Not.Null);
        }


        private async Task<DataBaseContext> GetDataBaseContext()
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
               new Mock<StockInvestmentRepository>(context).Object,
               new Mock<StockRepository>(context).Object
               );
            return service;
        }
    }
}
