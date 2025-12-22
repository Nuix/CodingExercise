using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using InvestmentPerformanceWebApi.Api.Controllers;
using InvestmentPerformanceWebApi.Services.Interfaces;
using InvestmentPerformanceWebApi.Api.ViewModels.Investments;

namespace InvestmentPerformanceApi.Tests.ControllerTests
{
    public class UserInvestmentsControllerTests
    {
        private readonly Mock<IInvestmentService> _mockInvestmentService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UserInvestmentsController>> _mockLogger;
        private readonly UserInvestmentsController _controller;

        public UserInvestmentsControllerTests()
        {
            _mockInvestmentService = new Mock<IInvestmentService>();
            _mockLogger = new Mock<ILogger<UserInvestmentsController>>();

            var configExpression = new MapperConfigurationExpression();
            configExpression.CreateMap<InvestmentPerformanceWebApi.Domain.Models.Investment, InvestmentSummaryView>();
            configExpression.CreateMap<InvestmentPerformanceWebApi.Domain.Models.Investment, InvestmentDetailView>();

            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            _mapper = mapperConfig.CreateMapper();

            _controller = new UserInvestmentsController(
                _mockLogger.Object,
                _mockInvestmentService.Object,
                _mapper
            );
        }

        [Fact]
        public async Task GetInvestmentsForUser_ReturnsOk_WithMappedDto()
        {
            // Arrange
            int userId = 1;
            var fixture = new Fixture();
            var investments = fixture.CreateMany<InvestmentPerformanceWebApi.Domain.Models.Investment>(3);


            // Use Task.FromResult to avoid Moq ReturnsAsync issues
            _mockInvestmentService
                .Setup(s => s.GetInvestmentsForUserAsync(userId))
                .Returns(Task.FromResult(investments));
            // Act
            var result = await _controller.GetInvestmentsForUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedDtos = Assert.IsAssignableFrom<IEnumerable<InvestmentSummaryView>>(okResult.Value);
            Assert.Equal(3, ((List<InvestmentSummaryView>)returnedDtos).Count);
        }
        [Fact]
        public async Task GetSingleInvestmentForUser_ReturnsOk_WithMappedDto()
        {
            // Arrange
            int userId = 1;

            var fixture = new Fixture();

            var investment = fixture.Create<InvestmentPerformanceWebApi.Domain.Models.Investment>();

            _mockInvestmentService
                .Setup(s => s.GetInvestmentForUserAsync(userId, investment.Id))
                .Returns(Task.FromResult(investment));

            // Act
            var result = await _controller.GetInvestmentForUser(userId, investment.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var dto = Assert.IsType<InvestmentDetailView>(okResult.Value);

            Assert.Equal(investment.Id, dto.Id);
            Assert.Equal(investment.Name, dto.Name);
        }


        [Fact]
        public async Task GetInvestmentsForUser_ReturnsNotFound_WhenInvestmentDoesNotExist()
        {
            // Arrange

            _mockInvestmentService
                .Setup(s => s.GetInvestmentsForUserAsync(It.IsAny<int>()))
                .ThrowsAsync(new KeyNotFoundException("Investment not found"));

            // Act
            var result = await _controller.GetInvestmentsForUser(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Contains("Investment not found", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task GetSingleInvestmentsForUser_ReturnsNotFound_WhenInvestmentDoesNotExist()
        {
            // Arrange
            _mockInvestmentService
                .Setup(s => s.GetInvestmentForUserAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new KeyNotFoundException("Investment not found"));

            // Act
            var result = await _controller.GetInvestmentForUser(1, 2);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Contains("Investment not found", notFoundResult.Value.ToString());
        }
    }
}
