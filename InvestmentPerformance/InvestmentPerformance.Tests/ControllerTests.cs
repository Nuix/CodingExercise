using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvestmentPerformance.Business;
using InvestmentPerformance.Business.Models;
using System;
using InvestmentPerformance.Data.Model;
using Moq;
using InvestmentPerformance.Host.Controllers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using InvestmentPerformance.Business.Models.APIResponses;
using System.Collections.Generic;

namespace InvestmentPerformance.Tests
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IBusinessService> _mockBusinessService { get; set; }

        public ControllerTests()
        {
            _mockBusinessService = new Mock<IBusinessService>();
        }

        [TestMethod]
        public async Task should_return_500_status_from_GetUserInvestmentsByUser_error()
        {
            _mockBusinessService.Setup(p => p.GetUserInvestmentsByUser(It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = new UserInvestmentsController(_mockBusinessService.Object, new Mock<ILogger<UserInvestmentsController>>().Object);

            var result = await controller.GetUserInvestmentsByUser(1);

            var objectResult = result as ObjectResult;

            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(500);
        }

        [TestMethod]
        public async Task should_return_500_status_from_GetUserInvestmentsDetails_error()
        {
            _mockBusinessService.Setup(p => p.GetUserInvestmentsDetails(It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = new UserInvestmentsController(_mockBusinessService.Object, new Mock<ILogger<UserInvestmentsController>>().Object);

            var result = await controller.GetUserInvestmentsDetails(1);

            var objectResult = result as ObjectResult;

            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(500);
        }

        [TestMethod]
        public async Task should_return_200_status_from_GetUserInvestmentsDetails()
        {
            _mockBusinessService.Setup(p => p.GetUserInvestmentsDetails(It.IsAny<int>())).ReturnsAsync(new GetUserInvestmentsDetailsResponse { UserInvestment = new UserInvestmentDetailsVM() });

            var controller = new UserInvestmentsController(_mockBusinessService.Object, new Mock<ILogger<UserInvestmentsController>>().Object);

            var result = await controller.GetUserInvestmentsDetails(1);

            var okObjectResult = result as OkObjectResult;

            okObjectResult.ShouldNotBeNull();
            okObjectResult.StatusCode.ShouldBe(200);
        }

        [TestMethod]
        public async Task should_return_200_status_from_GetUserInvestmentsByUser()
        {
            _mockBusinessService.Setup(p => p.GetUserInvestmentsByUser(It.IsAny<int>())).ReturnsAsync(new GetUserInvestmentsByUserResponse { Investments = new List<UserInvestmentVM>(), User = new UserVM() });

            var controller = new UserInvestmentsController(_mockBusinessService.Object, new Mock<ILogger<UserInvestmentsController>>().Object);

            var result = await controller.GetUserInvestmentsDetails(1);

            var okObjectResult = result as OkObjectResult;

            okObjectResult.ShouldNotBeNull();
            okObjectResult.StatusCode.ShouldBe(200);
        }


    }
}