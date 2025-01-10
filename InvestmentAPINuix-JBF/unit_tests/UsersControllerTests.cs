using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using InvestmentAPINuix_JBF.Models;
using InvestmentAPINuix_JBF.API;
using InvestmentAPINuix_JBF.Classes;

public class UsersControllerTests
{
    //private readonly Mock<ILogger<UsersController>> _mockLogger;
    //private readonly Mock<DbContext> _mockDBContext;

    [Fact]
    public void GetUserData_Test(){

        var options = new DbContextOptionsBuilder<InvestmentAPIDatabase>()
            .UseInMemoryDatabase(databaseName: "TestingDatabase").Options;
        
        using (var context= new InvestmentAPIDatabase(options)){
            context.Users.Add(new User {Id=1, Email = "TestUser@user.com", FirstName = "Tester", LastName="Johnson", Investments = new List<Investment>{}});
            context.Users.Add(new User {Id=2, Email = "TestUser2@user.com", FirstName = "Tester2", LastName="Smith", Investments = new List<Investment>{}});
            context.SaveChanges();
        }

        var mockLogger = new Mock<ILogger<UserDataController>>();
        using(var context = new InvestmentAPIDatabase(options))
        {
            
            var controller = new UserDataController(context, mockLogger.Object);

            var result = controller.GetAllUsers();

            var assertResultType = Assert.IsType<ActionResult<User>>(result);
            var okResult = Assert.IsType<OkObjectResult>(assertResultType.Result);
            var resultValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, resultValue.Count);
            Assert.Equal("TestUser@user.com", resultValue[0].Email);
            
        }
    }
}