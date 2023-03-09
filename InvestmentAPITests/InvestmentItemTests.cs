using InvestmentAPI;
using System.Diagnostics.CodeAnalysis;

namespace InvestmentAPITests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentItemTests
    {
        private string _name = "name";
        private Guid _investmentGuid = Guid.NewGuid();

        [TestMethod]
        public void Test_InvestmentItem_DefualtConstructor()
        {
            var investmentItem = new InvestmentItem();
            Assert.IsNotNull(investmentItem);
        }
        [TestMethod]
        public void Test_InvestmentItem_Id()
        {
            var investmentItem = new InvestmentItem();
            investmentItem.Id = _investmentGuid;
            Assert.AreEqual(_investmentGuid, investmentItem.Id);
        }
        [TestMethod]
        public void Test_InvestmentItem_Name()
        {
            var investmentItem = new InvestmentItem();
            investmentItem.Name = _name;
            Assert.AreEqual(_name, investmentItem.Name);
        }

        [TestMethod]
        public void Test_InvestmentItem_ParameterizedConstructor()
        {
            var investmentItem = new InvestmentItem(_investmentGuid, _name);
            Assert.IsNotNull(investmentItem);
            Assert.AreEqual(_investmentGuid, investmentItem.Id);
            Assert.AreEqual(_name, investmentItem.Name);
        }
    }
}