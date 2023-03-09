using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace InvestmentAPI
{
    public class InvestmentAPI : IInvestmentAPI
    {
        private IInvestmentDAL _investmentDal;

        public InvestmentAPI(IDbConnection connection, ILogger logger)
        {
            _investmentDal = new InvestmentDAL(connection, logger);
        }

        public InvestmentRecord GetInvestmentRecord(Guid investmentId)
        {
            return _investmentDal.GetInvestmentRecordByInvestmentId(investmentId);
        }

        public UsersInvestments GetUserInvestments(Guid userId)
        {
            UsersInvestments usersInvestments = new UsersInvestments();
            usersInvestments.UserId = userId;
            usersInvestments.Investments = _investmentDal.GetInvestmentItemsByUserId(userId);
            return usersInvestments;
        }
    }
}
