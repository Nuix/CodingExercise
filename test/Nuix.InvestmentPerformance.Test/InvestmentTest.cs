using System;
using Xunit;
using Moq;
using Nuix.InvestmentPerformance.Data.Interfaces;
using Nuix.InvestmentPerformance.Data.Dtos;
using Nuix.InvestmentPerformance.Web.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Nuix.InvestmentPerformance.Test
{
    public class InvestmentTest
    {
        public Mock<IStockConnector> _stockConnector = new Mock<IStockConnector>();
        public Mock<IUserConnector> _userConnector = new Mock<IUserConnector>();
        public Mock<IInvestmentConnector> _investmentConnector = new Mock<IInvestmentConnector>();
        public Mock<IPriceConnector> _priceConnector = new Mock<IPriceConnector>();

        [Fact]
        public void GetUserInvestments()
        {
            //_stockConnector.Setup(s => s.GetStock("TEST")).Returns(new StockDto { Name = "Test Corp", Symbol = "Test" });
            _userConnector.Setup(u => u.GetUser(1)).Returns(new UserDto { UserId = 1, Name = "Lucas Folino" });
            _investmentConnector.Setup(i => i.GetUserInvestments(1)).Returns(new List<InvestmentDto> { new InvestmentDto { UserId = 1, Symbol = "AAPL",InvestmentId=1 } });
            UserController userController = new UserController(new NullLogger<UserController>(), _userConnector.Object, _investmentConnector.Object);
            var response = userController.GetUserInvestments(1);
            var jsonResult = response as JsonResult;            
            var investments = jsonResult.Value as List<InvestListItemDto>;
            Assert.Single(investments);
            var investment = investments.First();
            Assert.Equal("AAPL", investment.Name);
            Assert.Equal(1, investment.InvestmentId);

        }

        [Fact]
        public void GetInvestmentGainsShortTerm()
        {
            _investmentConnector.Setup(i => i.GetInvestment(1)).Returns( new InvestmentDto { UserId = 1, Symbol = "AAPL", InvestmentId = 1,CostBasis=1000,NumberOfShares=42,PurchaseDate=new DateTime(2021,3,1) });
            _priceConnector.Setup(p => p.GetCurrentPrice("AAPL")).Returns(1125.25M);
            InvestmentController investmentController = new InvestmentController(new NullLogger<InvestmentController>(), _investmentConnector.Object, _stockConnector.Object, _priceConnector.Object);
            var response = investmentController.GetInvestment(1);
            var jsonResult = response as JsonResult;
            var investment = jsonResult.Value as InvestmentDto;
            Assert.NotNull(investment);
            Assert.Equal(5260.50M, investment.TotalGain);
            Assert.Equal(47260.50M, investment.Value);
        }

        [Fact]
        public void GetInvestmentLossLongTerm()
        {
            _investmentConnector.Setup(i => i.GetInvestment(1)).Returns(new InvestmentDto { UserId = 1, Symbol = "AAPL", InvestmentId = 1, CostBasis = 1500, NumberOfShares = 42, PurchaseDate = new DateTime(2020, 3, 1) });
            _priceConnector.Setup(p => p.GetCurrentPrice("AAPL")).Returns(1125.25M);
            InvestmentController investmentController = new InvestmentController(new NullLogger<InvestmentController>(), _investmentConnector.Object, _stockConnector.Object, _priceConnector.Object);
            var response = investmentController.GetInvestment(1);
            var jsonResult = response as JsonResult;
            var investment = jsonResult.Value as InvestmentDto;
            Assert.NotNull(investment);
            Assert.Equal(-15739.5M, investment.TotalGain);
            Assert.Equal(47260.50M, investment.Value);
        }
    }
}
