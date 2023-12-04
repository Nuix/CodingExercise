using InvestmentPerformanceApi.Controllers;
using InvestmentPerformanceApi.Models;
using InvestmentPerformanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace InvestmentPerformanceApi.Tests
{
    public class InvestmentPerformanceControllerTest
    {
        [Fact]
        public async void getUserInvestments_shouldErrorBadRequest()
        {
            var service = new Mock<IInvestmentService>();
            service.Setup(foo => foo.getUserInvestmentsAsync(0)).ReturnsAsync((string)null);
            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetUserInvestments(0);
            Assert.IsType<BadRequestObjectResult>(result);

            BadRequestObjectResult brResult = result as BadRequestObjectResult;
            Assert.Equal("The userId provided could not be found.", brResult.Value);
           
        }

        [Fact]
        public async void getUserInvestments_shouldReturnInvestments()
        {
            var service = new Mock<IInvestmentService>();
            var expected = new
            {
                UserId = 1,
                UserName = "Test User",
                Investments = new List<object>
                        {
                            new
                            {
                                InvestmentId = 1,
                                StockName = "Test Stock"
                            },
                            new
                            {
                                InvestmentId = 2,
                                StockName = "Test Stock 2"
                            },
                            new { InvestmentId = 3, StockName = "Test Stock 3"}
                        }
            };
            service.Setup(foo => foo.getUserInvestmentsAsync(1)).ReturnsAsync(JsonConvert.SerializeObject(expected, Formatting.Indented));
            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetUserInvestments(1);

           Assert.IsType<OkObjectResult>(result);
           OkObjectResult okResult = (OkObjectResult)result;
           Assert.Equal(JsonConvert.SerializeObject(expected, Formatting.Indented), okResult.Value);
        }

        [Fact]
        public async void getUserInvestments_shouldReturnUserButNoInvestments()
        {
            var service = new Mock<IInvestmentService>();
            var expected = new
            {
                UserId = 3,
                UserName = "Test User",
            };
            service.Setup(foo => foo.getUserInvestmentsAsync(3)).ReturnsAsync(JsonConvert.SerializeObject(expected, Formatting.Indented));
            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetUserInvestments(3);
             Assert.IsType<OkObjectResult>(result);
              OkObjectResult okResult = (OkObjectResult)result;
             Assert.Equal(JsonConvert.SerializeObject(expected, Formatting.Indented), okResult.Value);
        }

        [Fact]
        public async void getInvestmentForUser_invalidUserId()
        {
            var service = new Mock<IInvestmentService>();
            service.Setup(foo => foo.getInvestmentDetails(0, 0)).ReturnsAsync((string)null);
            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetInvestmentDetails(0,0);
            Assert.IsType<ActionResult<Investment>>(result);
            ActionResult<Investment> actionResult = (ActionResult<Investment>)result;
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.Equal("The userId or investmentId provided cannot be found.", badRequest?.Value);
        }

        [Fact]
        public async void getInvestmentForUser_invalidInvestmentId()
        {
            var service = new Mock<IInvestmentService>();
            service.Setup(foo => foo.getInvestmentDetails(1, 0)).ReturnsAsync((string)null);
            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetInvestmentDetails(1, 0);
            Assert.IsType<ActionResult<Investment>>(result);
            ActionResult<Investment> actionResult = (ActionResult<Investment>)result;
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = actionResult.Result as BadRequestObjectResult;
            Assert.Equal("The userId or investmentId provided cannot be found.", badRequest?.Value);
        }

        [Fact]
        public async void getInvestmentForUser_validInputs()
        {
            var service = new Mock<IInvestmentService>();
            Investment expected = new Investment();
            expected.InvestmentId = 1;
            expected.UserId = 1;
            expected.StockName = "Tech Stock A";
            expected.CurrentPricePerShare = 100;
            expected.PurchaseDate = DateTime.Now;
            expected.Shares = 50;
            expected.PurchasePrice = 150;
            expected.User = new User()
            {
                UserId = 1,
                UserName = "Tyler Pingree"
            };


            service.Setup(foo => foo.getInvestmentDetails(1, 1)).ReturnsAsync(JsonConvert.SerializeObject(expected, Formatting.Indented));



            var controller = new InvestmentPerformanceController(service.Object);

            var result = await controller.GetInvestmentDetails(1, 1);
            Assert.IsType<ActionResult<Investment>>(result);
            ActionResult<Investment> actionResult = (ActionResult<Investment>)result;
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var okRequest = actionResult.Result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(expected, Formatting.Indented), okRequest?.Value);
        }
    }
}
