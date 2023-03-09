using InvestmentAPI;
using System.Diagnostics.CodeAnalysis;

namespace InvestmentAPITests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UserInvestmentsTests
    {
        Guid _userGuid = Guid.NewGuid();
        Guid _investmentGuid = Guid.NewGuid();
        string _name = "name";

        [TestMethod]
        public void Test_UserInvestments_DefaultConstructor()
        {
            var userInvestments = new UsersInvestments();
            Assert.IsNotNull(userInvestments);
        }

        [TestMethod]
        public void Test_UserInvestments_UserId()
        {
            var userInvestments = new UsersInvestments();
            userInvestments.UserId = _userGuid;
            Assert.AreEqual(_userGuid, userInvestments.UserId);
        }

        [TestMethod]
        public void Test_UserInvestments_Investments()
        {
            var investmentList = new List<InvestmentItem>() { new InvestmentItem(_investmentGuid, _name) };
            var userInvestments = new UsersInvestments();
            userInvestments.Investments = investmentList;
            Assert.AreEqual(investmentList, userInvestments.Investments);
        }

        [TestMethod]
        public void Test_UserInvestments_ParameterizedConstructor()
        {
            var investmentList = new List<InvestmentItem>() { new InvestmentItem(_investmentGuid, _name) };
            var userInvestments = new UsersInvestments(_userGuid, investmentList);
            Assert.IsNotNull(userInvestments);
            Assert.AreEqual(investmentList, userInvestments.Investments);
            Assert.AreEqual(_userGuid, userInvestments.UserId);
        }
    }
}
