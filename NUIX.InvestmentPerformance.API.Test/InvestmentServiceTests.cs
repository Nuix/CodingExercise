using Microsoft.EntityFrameworkCore;
using Moq;
using NUIX.InvestmentPerformance.API.Data;
using NUIX.InvestmentPerformance.API.Models;
using NUIX.InvestmentPerformance.API.Models.Enums;
using NUIX.InvestmentPerformance.API.Services;

namespace NUIX.InvestmentPerformance.API.Test
{
    public class InvestmentServiceTests
    {

        [TestCase( "11111111-1111-1111-1111-111111111111","CFD2EC55-EC50-4160-AADC-18DB56C2E608", "Alphabet Inc. (Google)")]
        [Test]
        public void GetInvestmentDetails_ShouldCalculateCorrectValues(string userID, string investmentID, string investmentName)
        {
            // Arrange
            var investment = new Investment
            {
                InvestmentID = Guid.Parse(investmentID),
                UserID = Guid.Parse(userID),
                InvestmentName = investmentName,
                NumberOfShares = 10,
                CostBasisPerShare = 150m,
                CurrentPricePerShare = 180m,
                PurchaseDate = DateTime.Now.AddMonths(-18)
            };
            var service = new InvestmentService();

            // Act
            var result = service.CalculateInvestmentDetails(investment);

            // Assert
            Assert.AreEqual(1800m, result.CurrentValue);
            Assert.AreEqual(InvestmentTerm.LongTerm, result.Term);
            Assert.AreEqual(300m, result.TotalGainOrLoss);         
        }
    }
}