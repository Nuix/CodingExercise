using InvestmentAPI;
using System.Diagnostics.CodeAnalysis;

namespace InvestmentAPITests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentRecordTests
    {
        long numberOfShares = 3;
        decimal costBasisPerShare = 1.11m;
        decimal currentPrice = 2.22m;

        [TestMethod]
        public void Test_InvestmentRecord_DefaultConstructor()
        {
            var investmentRecord = new InvestmentRecord();
            Assert.IsNotNull(investmentRecord);
        }

        [TestMethod]
        public void Test_InvestmentRecord_NumberOfShares()
        {
            var investmentRecord = new InvestmentRecord() { NumberOfShares = numberOfShares };
            Assert.AreEqual(numberOfShares, investmentRecord.NumberOfShares);
        }

        [TestMethod]
        public void Test_InvestmentRecord_CostBasisPerShare()
        {
            var investmentRecord = new InvestmentRecord() { CostBasisPerShare = 1.11m };
            Assert.AreEqual(1.11m, investmentRecord.CostBasisPerShare);
        }

        [TestMethod]
        public void Test_InvestmentRecord_CurrentPrice()
        {
            var investmentRecord = new InvestmentRecord() { CurrentPrice = 2.22m };
            Assert.AreEqual(2.22m, investmentRecord.CurrentPrice);
        }

        [TestMethod]
        public void Test_InvestmentRecord_ParameterizedConstructor()
        {
            var purchaseDate = DateTime.UtcNow.AddDays(-1);

            var investmentRecord = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, purchaseDate);
            Assert.AreEqual(numberOfShares, investmentRecord.NumberOfShares);
            Assert.AreEqual(costBasisPerShare, investmentRecord.CostBasisPerShare);
            Assert.AreEqual(currentPrice, investmentRecord.CurrentPrice);
            Assert.AreEqual(Term.Short, investmentRecord.Term);
        }

        [TestMethod]
        public void Test_InvestmentRecord_LongTerm()
        {
            var purchaseDate = DateTime.UtcNow.AddDays(-1);
            purchaseDate = purchaseDate.AddYears(-1);
            var investmentRecord = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, purchaseDate);
            Assert.AreEqual(Term.Long, investmentRecord.Term);
        }

        [TestMethod]
        public void Test_InvestmentRecord_ShortTerm_1Year()
        {
            var purchaseDate = DateTime.UtcNow.AddYears(-1);
            var investmentRecord = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, purchaseDate);
            Assert.AreEqual(Term.Short, investmentRecord.Term);
        }

        [TestMethod]
        public void Test_InvestmentRecord_CurrentValue()
        {
            var investmentRecord = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, DateTime.UtcNow);
            Assert.AreEqual(numberOfShares * currentPrice, investmentRecord.CurrentValue);
        }

        [TestMethod]
        public void Test_InvestMesntRecord_TotalGainLoss()
        {
            var totalGainLoss = (numberOfShares * currentPrice) - (numberOfShares * costBasisPerShare);
            var investmentRecord = new InvestmentRecord(numberOfShares, costBasisPerShare, currentPrice, DateTime.UtcNow);
            Assert.AreEqual(totalGainLoss, investmentRecord.TotalGainLoss);
        }
    }
}
