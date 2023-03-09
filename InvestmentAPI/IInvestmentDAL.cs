using System;
using System.Collections.Generic;

namespace InvestmentAPI
{
    internal interface IInvestmentDAL
    {
        List<InvestmentItem> GetInvestmentItemsByUserId(Guid userId);
        InvestmentRecord GetInvestmentRecordByInvestmentId(Guid investmentId);
    }
}
