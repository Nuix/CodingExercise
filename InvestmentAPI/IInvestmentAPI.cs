using System;

namespace InvestmentAPI
{
    public interface IInvestmentAPI
    {
        UsersInvestments GetUserInvestments(Guid userId);
        InvestmentRecord GetInvestmentRecord(Guid investmentId);
    }
}
