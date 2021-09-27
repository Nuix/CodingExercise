using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using InvestmentPerformance.Web.Enums;
using InvestmentPerformance.Web.Models.Entities;
using InvestmentPerformance.Web.Repository;
using InvestmentPerformance.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace InvestmentPerformance.Tests
{
    public class InvestmentsServiceTests
    {
        private Mock<IInvestmentsRepository> storage;
        private Mock<IInvestmentPriceService> investmentPriceService;
        private IInvestmentsService investmentsService;
        private Mock<ILogger<InvestmentsService>> logger;
        private UserInvestments storageInvestment = null;
        
        [SetUp]
        public void Setup()
        {
            storage = new Mock<IInvestmentsRepository>(MockBehavior.Default);
            investmentPriceService = new Mock<IInvestmentPriceService>(MockBehavior.Default);
            logger = new Mock<ILogger<InvestmentsService>>(MockBehavior.Default);
            investmentsService = new InvestmentsService(storage.Object,
                investmentPriceService.Object,
                logger.Object);

            storageInvestment = new UserInvestments()
            {
                Id = 1,
                Investment = new Investment()
                {
                    Id = 1,
                    Name = "AAPL"
                },
                Quantity = 1,
                CostBasis = 10,
                AcquireDateUtc = DateTimeOffset.UtcNow,
                SellDateUtc = DateTimeOffset.UtcNow,
                InvestmentStatus = InvestmentStatus.Active
            };
        }

        [Test]
        public void GetInvestmentsByUserIdAsync_ThrowsException_WhenStorageBrokerReturnsNull()
        {
            // When // Should
            investmentsService
                .Invoking(t => t.GetInvestmentsByUserIdAsync(1))
                .Should()
                .Throw<Exception>()
                .WithMessage("Received null response while getting investments for 1");
        }
        
        [Test]
        public async Task GetInvestmentsByUserIdAsync_ReturnsInvestmentList_GivenStorageReturnsInvestments()
        {
            // Given
            var investments = new List<UserInvestments>();
            investments.Add(storageInvestment);
            
            // When
            storage.Setup(t => t.SelectInvestmentsByUserIdAsync(1))
                .Returns(Task.FromResult(investments));

            var response = await investmentsService.GetInvestmentsByUserIdAsync(1);

            // Should
            response[0].Id.Should().Be(1);
            response[0].Name.Should().Be("AAPL");
        }
        
        [Test]
        public void GetInvestmentsByUserIdAndInvestmentIdAsync_ThrowsException_WhenStorageBrokerReturnsNull()
        {
            // When // Should
            investmentsService
                .Invoking(t => t.GetInvestmentsByUserIdAndInvestmentIdAsync(1, 1))
                .Should()
                .Throw<Exception>()
                .WithMessage("Received null response while getting investment details for user 1 and investment 1");
        }
        
        [Test]
        public async Task GetInvestmentsByUserIdAndInvestmentIdAsync_ReturnsInvestmentDetails_GivenStorageReturnsInvestments()
        {
            // Given
            var investments = new List<UserInvestments>();
            investments.Add(storageInvestment);
            
            // When
            storage.Setup(t => t.SelectInvestmentsByUserIdAndInvestmentIdAsync(1, 1))
                .Returns(Task.FromResult(storageInvestment));

            investmentPriceService.Setup(t => t.GetPriceAsync(It.IsAny<string>(), It.IsAny<InvestmentType>()))
                .Returns(Task.FromResult<double>(10));
            

            var response = await investmentsService.GetInvestmentsByUserIdAndInvestmentIdAsync(1, 1);

            // Should
            response.Quantity.Should().Be(storageInvestment.Quantity);
            response.CostBasis.Should().Be(storageInvestment.CostBasis);
            response.CurrentPrice.Should().Be(10);
            response.CurrentValue.Should().Be(response.Quantity * 10);
            response.InvestmentTerm.Should().Be(InvestmentTerm.ShortTerm);
            response.Gain.Should().Be(0);
        }
    }
}