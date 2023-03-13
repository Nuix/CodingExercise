using CodingExercise.Controllers;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CodingExercise.ControllerTests
{
    [TestFixture]
    public class InvestmentControllerTests
    {
        private Mock<IInvestmentService> _investmentService = new Mock<IInvestmentService>();
        private Mock<IUserService> _userService = new Mock<IUserService>();
        private static readonly Stock _exampleStock = new Stock(name: "stock", price: 5);
        private readonly Investment _exampleInvestment = new Investment(stock:  _exampleStock, quantity: 2);
        private readonly User _exampleUser = new User();
        private readonly StockInvestment _exampleStockInvestment = new StockInvestment();

        public InvestmentControllerTests()
        {

        }

        #region GetInvestment

        [Test]
        public void GetInvestment()
        {
            _investmentService.Setup(x => x.GetInvestment(It.IsAny<int>())).Returns(_exampleInvestment);
            var controller = new InvestmentController(_userService.Object, _investmentService.Object);
            Assert.DoesNotThrow(() => controller.GetInvestment(1), "Did not expect call to throw exception");
        }

        [Test]
        public void GetInvestment_InvestmentDoesNotExist()
        {
            _investmentService.Setup(x => x.GetInvestment(It.IsAny<int>())).Returns<Investment>(null);
            var controller = new InvestmentController(_userService.Object, _investmentService.Object);
            var result = controller.GetInvestment(1);
            Assert.IsInstanceOf<NotFoundResult>(result.Result, "Expected exception");
        }

        #endregion GetInvestment

        #region GetUserInvestments

        [Test]
        public void GetUserInvestments()
        {
            _userService.Setup(x => x.GetUser(It.IsAny<int>())).Returns(_exampleUser);
            _investmentService.Setup(x => x.GetStockInvestmentsForUser(It.IsAny<int>())).Returns(new List<StockInvestment>() { _exampleStockInvestment });
            var controller = new InvestmentController(_userService.Object, _investmentService.Object);
            Assert.DoesNotThrow(() => controller.GetUserInvestments(1), "Did not expect call to throw exception");
        }

        [Test]
        public void GetUserInvestments_UserDoesNotExist()
        {
            _userService.Setup(x => x.GetUser(It.IsAny<int>())).Returns<User>(null);
            _investmentService.Setup(x => x.GetStockInvestmentsForUser(It.IsAny<int>())).Returns(new List<StockInvestment>() { _exampleStockInvestment });
            var controller = new InvestmentController(_userService.Object, _investmentService.Object);
            var result = controller.GetUserInvestments(1);
            Assert.IsInstanceOf<NotFoundResult>(result.Result, "Expected exception");
        }

        [Test]
        public void GetUserInvestments_UserHasNoInvestments()
        {
            _userService.Setup(x => x.GetUser(It.IsAny<int>())).Returns(_exampleUser);
            _investmentService.Setup(x => x.GetStockInvestmentsForUser(It.IsAny<int>())).Returns(new List<StockInvestment>() { });
            var controller = new InvestmentController(_userService.Object, _investmentService.Object);
            Assert.DoesNotThrow(() => controller.GetUserInvestments(1), "Did not expect call to throw exception");
        }

        #endregion GetUserInvestments
    }
}
