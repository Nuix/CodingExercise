using AutoMapper;
using InvestmentPerformance.Controllers;
using InvestmentPerformance.DTOs;
using InvestmentPerformance.Models;
using InvestmentPerformance.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InvestmentPerformance.Tests.Controllers
{
    [TestClass]
    public class InvestmentsControllerTests
    {
        private readonly Mock<IMapper> _mapper;

        private readonly Mock<IInvestmentRepository> _investmentRepository;

        private readonly InvestmentsController _controller;

        private readonly HttpContext _httpContext;

        public InvestmentsControllerTests()
        {
            _mapper = new Mock<IMapper>();
            _investmentRepository = new Mock<IInvestmentRepository>();
            _controller = new InvestmentsController(_mapper.Object, _investmentRepository.Object);

            _httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContext
            };
            _httpContext.Request.Headers["UserID"] = "1"; // Set a default UserID header for tests
        }

        [TestMethod]
        public void GetInvestments_ReturnsInvestments()
        {
            // Arrange
            var userId = 1;
            var investments = new List<Investment>
            {
                new() { ID = 1, UserID = userId, StockID = 1, SharesOwned = 10, CostBasisPerShare = 100.00m, PurchaseDate = DateTime.Now.AddMonths(-5) },
                new() { ID = 2, UserID = userId, StockID = 2, SharesOwned = 20, CostBasisPerShare = 200.00m, PurchaseDate = DateTime.Now.AddYears(-2) }
            };
            _investmentRepository.Setup(repo => repo.GetInvestmentsByUserId(userId)).ReturnsAsync(investments);
            _mapper.Setup(m => m.Map<List<InvestmentDto>>(It.IsAny<IEnumerable<Investment>>()))
                   .Returns(new List<InvestmentDto>
                   {
                       new() { ID = 1, StockName = "Stock1" },
                       new() { ID = 2, StockName = "Stock2" }
                   });

            // Act
            var result = _controller.GetInvestments().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result?.Value?.Count());
        }

        [TestMethod]
        public void GetInvestments_ReturnsBadRequest_WhenUserIdHeaderIsMissing()
        {
            // Arrange
            _httpContext.Request.Headers.Clear(); // Ensure no UserID header is present

            // Act
            var result = _controller.GetInvestments().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("UserID header is required.", badRequestResult.Value);
        }

        [TestMethod]
        public void GetInvestment_ReturnsInvestmentDetails()
        {
            // Arrange
            var userId = 1;
            var investmentId = 1;
            var investment = new Investment
            {
                ID = investmentId,
                UserID = userId,
                StockID = 1,
                SharesOwned = 10,
                CostBasisPerShare = 100.00m,
                PurchaseDate = DateTime.Now.AddMonths(-5)
            };
            _investmentRepository.Setup(repo => repo.GetInvestmentById(investmentId)).ReturnsAsync(investment);
            _mapper.Setup(m => m.Map<InvestmentDetailsDto>(It.IsAny<Investment>()))
                   .Returns(new InvestmentDetailsDto
                   {
                       ID = investmentId,
                       CostBasisPerShare = 100.00m,
                       CurrentValue = 1500.00m,
                       CurrentPrice = 150.00m,
                       Term = "Short-Term",
                       TotalGainOrLoss = 500.00m,
                   });

            // Act
            var result = _controller.GetInvestment(investmentId).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(InvestmentDetailsDto));
            Assert.AreEqual(investmentId, result.Value.ID);
        }

        [TestMethod]
        public void GetInvestment_ReturnsBadRequest_WhenHeaderIsMissing()
        {
            // Arrange
            _httpContext.Request.Headers.Clear(); // Ensure no UserID header is present

            // Act
            var result = _controller.GetInvestment(5).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("UserID header is required.", badRequestResult.Value);
        }

        [TestMethod]
        public void GetInvestment_ReturnsNotFound_WhenInvestmentDoesNotExist()
        {
            // Arrange
            var userId = 1;
            var investmentId = 1;
            var investment = new Investment
            {
                ID = investmentId,
                UserID = userId,
                StockID = 1,
                SharesOwned = 10,
                CostBasisPerShare = 100.00m,
                PurchaseDate = DateTime.Now.AddMonths(-5)
            };
            _investmentRepository.Setup(repo => repo.GetInvestmentById(investmentId)).ReturnsAsync(investment);
            _mapper.Setup(m => m.Map<InvestmentDetailsDto>(It.IsAny<Investment>()))
                   .Returns(new InvestmentDetailsDto
                   {
                       ID = investmentId,
                       CostBasisPerShare = 100.00m,
                       CurrentValue = 1500.00m,
                       CurrentPrice = 150.00m,
                       Term = "Short-Term",
                       TotalGainOrLoss = 500.00m,
                   });

            // Act
            var result = _controller.GetInvestment(investmentId + 1).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetInvestment_ReturnsUnauthorized_WhenAccessingOthersInvestment()
        {
            // Arrange
            var userId = 1;
            var investmentId = 1;
            var investment = new Investment
            {
                ID = investmentId,
                UserID = userId + 1, // Different user
                StockID = 1,
                SharesOwned = 10,
                CostBasisPerShare = 100.00m,
                PurchaseDate = DateTime.Now.AddMonths(-5)
            };
            _investmentRepository.Setup(repo => repo.GetInvestmentById(investmentId)).ReturnsAsync(investment);
            _mapper.Setup(m => m.Map<InvestmentDetailsDto>(It.IsAny<Investment>()))
                   .Returns(new InvestmentDetailsDto
                   {
                       ID = investmentId,
                       CostBasisPerShare = 100.00m,
                       CurrentValue = 1500.00m,
                       CurrentPrice = 150.00m,
                       Term = "Short-Term",
                       TotalGainOrLoss = 500.00m,
                   });

            // Act
            var result = _controller.GetInvestment(investmentId).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }
    }
}
