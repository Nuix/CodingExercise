using Microsoft.Extensions.Logging;
using Moq;
using Nuix_Project.Controllers;
using Nuix_Project.Data;
using NUnit.Framework;
using System;

namespace Unit_Tests
{
    public class NuixTests
    {
        private InvestmentController controller { get; set; }

       [SetUp]
        public void Setup()
        {
            var logger = Mock.Of<ILogger<InvestmentController>>();

            var dataLayer = Mock.Of<MockDataLayer>();

            var loginLayer = Mock.Of<MockLoginLayer>();

            controller = new InvestmentController(logger, dataLayer, loginLayer);
        }

        //These wont do much since they are running off the mock. But we will hit the calculations we made.

        [Test]
        [TestCase("FakeToken",1)]
        [TestCase("FakeToken", 2)]
        public void IsCurrentValueCorrect(string token, long investmentId)
        {
           
            var investmentTest =  controller.GetInvestmentDetailsByInvestmentId(token, investmentId);

            var calculatedCurrentValue = (decimal)(investmentTest.CurrentPrice * investmentTest.NumberOfShares);

            Assert.IsTrue(investmentTest.CurrentValue == calculatedCurrentValue);

            Assert.IsNotNull(investmentTest);

        }
        [Test]
        [TestCase("FakeToken", 1)]
        [TestCase("FakeToken", 2)]
        public void IsGainLossCorrect(string token, long investmentId)
        {

            var investmentTest = controller.GetInvestmentDetailsByInvestmentId(token, investmentId);

            var purchaseValue = (decimal)(investmentTest.CostBasisPerShare * investmentTest.NumberOfShares);

            var calculatedGainLoss = (investmentTest.CurrentValue - purchaseValue);

            Assert.IsTrue(investmentTest.GainLoss == calculatedGainLoss);

            Assert.IsNotNull(investmentTest);
           
        }

        [Test]
        [TestCase("FakeToken", 1)]
        [TestCase("FakeToken", 2)]
        public void IsTermCorrect(string token, long investmentId)
        {

            var investmentTest = controller.GetInvestmentDetailsByInvestmentId(token, investmentId);

            var term = (investmentTest.PurchaseDateTime.AddYears(1) >= DateTime.Now) ? "Short Term" : "Long Term";

            Assert.AreEqual(investmentTest.Term, term);

            Assert.IsNotNull(investmentTest);
          
        }
    }
}