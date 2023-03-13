using CodingExercise.Models;
using EnumsNET;
using NUnit.Framework;

namespace CodingExercise.ModelsTests
{
    [TestFixture]
    public class InvestmentTests
    {

        #region Value

        [TestCase(0, 4)]
        [TestCase(1, 0)]
        [TestCase(5, 68)]
        [TestCase(7, 9)]
        [TestCase(10, 2.15)]
        public void CalculateValue(int quantity, double stockPrice)
        {
            var investment = new Investment(stock: new Stock(name: "stock", price: stockPrice), quantity: quantity);
            var expectedValue = (quantity * stockPrice);

            Assert.AreEqual(expectedValue, investment.Value, $"Unexpected value returned");
        }

        #endregion Value

        #region Returns

        [TestCase(0, 4, 1)]
        [TestCase(0, 4, 0)]
        [TestCase(1, 0, 3.45)]
        [TestCase(5, 68, 7.88)]
        [TestCase(10, 2.15, 2.02)]
        public void CalculateReturns(int quantity, double costBasis, double stockPrice)
        {
            var investment = new Investment(stock: new Stock(name: "stock", price: stockPrice), quantity: quantity);
            investment.CostBasis = costBasis;
            var expectedReturns = (quantity * stockPrice) - (quantity * costBasis);

            Assert.AreEqual(expectedReturns, investment.Returns, $"Unexpected returns returned");
        }

        #endregion Returns

        #region TermType

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(350)]
        [TestCase(400)]
        [TestCase(1050)]
        public void CalculateTermType(int daysSinceAcquired)
        {
            var expectedTermType = TermType.ShortTerm;
            if (daysSinceAcquired > 365)
            {
                expectedTermType = TermType.LongTerm;
            }
            var expectedResponse = expectedTermType.AsString(EnumFormat.Description);

            var investment = new Investment(stock: new Stock(name: "stock", price: 5 ), quantity: 3);
            investment.AcquiredDate = investment.AcquiredDate.AddDays(-daysSinceAcquired);

            Assert.AreEqual(expectedResponse, investment.TermType, $"Unexpected term type returned");
        }

        #endregion TermType

        #region CurrentPrice

        [TestCase(4)]
        [TestCase(0)]
        [TestCase(68)]
        [TestCase(2.15)]
        public void CalculateCurrentPrice(double stockPrice)
        {
            var investment = new Investment(stock: new Stock(name: "stock", price: stockPrice), quantity: 5);
            Assert.AreEqual(stockPrice, investment.CurrentPrice, $"Unexpected value returned");
        }

        #endregion CurrentPrice
    }
}
