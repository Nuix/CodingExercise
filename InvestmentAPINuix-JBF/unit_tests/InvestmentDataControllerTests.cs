using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using InvestmentAPINuix_JBF.Models;
using InvestmentAPINuix_JBF.API;
using InvestmentAPINuix_JBF.Classes;

public class InvestmentDataControllerTests
{
    [Fact]
    public void GetInvestmentsPerUser_Test(){

        var options = new DbContextOptionsBuilder<InvestmentAPIDatabase>()
            .UseInMemoryDatabase(databaseName: "TestingDatabase").Options;
        
        using (var context= new InvestmentAPIDatabase(options)){
            context.Investments.Add(new Investment {Id=1, Company = "Google", NumberOfShares = 5, CostBasisPerShare=10.52M, CurrentPrice = 12.52M, DatePurchased = new DateOnly(2023, 11, 12), Term = "Short Term", StartingValue = 52.50M, CurrentValue = 64.50M, TotalValue = 12.00M, Investor = 1});
            context.SaveChanges();
        }

        var mockLogger = new Mock<ILogger<InvestmentDataController>>();
        using(var context = new InvestmentAPIDatabase(options))
        {
            
            var controller = new InvestmentDataController(context, mockLogger.Object);

            var result = controller.GetAllInvestmentsForUser(1);

            var assertResultType = Assert.IsType<ActionResult<Investment>>(result);
            var okResult = Assert.IsType<OkObjectResult>(assertResultType.Result);
            var resultValue = Assert.IsType<List<Investment>>(okResult.Value);
            Assert.Equal(1, resultValue.Count);
            Assert.Equal("Google", resultValue[0].Company);
            
            var investments = context.Investments.ToList();
            context.Investments.RemoveRange(investments);
            context.SaveChanges();
        }
    }

    [Fact]
    public void GetInvestmentsPerUser_NoInvestmentFound_Test()
    {
        var options = new DbContextOptionsBuilder<InvestmentAPIDatabase>()
            .UseInMemoryDatabase(databaseName: "TestingDatabase").Options;

        using (var context = new InvestmentAPIDatabase(options))
        {
            var mockLogger = new Mock<ILogger<InvestmentDataController>>();
            var controller = new InvestmentDataController(context, mockLogger.Object);

            // Use a user id that would return 0 investments
            var result = controller.GetAllInvestmentsForUser(10000000);

            var assertResultType = Assert.IsType<ActionResult<Investment>>(result);
            Assert.IsType<NoContentResult>(assertResultType.Result);
        }
    }

}