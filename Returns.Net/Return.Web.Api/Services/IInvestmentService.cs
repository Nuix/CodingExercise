using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Returns.Models;

namespace Return.Web.Api.Services;

public interface IInvestmentService
{
    /// <summary></summary>
    /// <param name="userId">UserId to get investments for</param>
    /// <returns>Returns null if no user found</returns>
    Task<IReadOnlyList<InvestmentRes>> GetByUserId(Guid userId, CancellationToken cts = default);
    
    /// <summary></summary>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<IReadOnlyList<InvestmentRes>> Query(InvestmentQuery query, CancellationToken cts = default);
}