using System;
using System.Threading;
using System.Threading.Tasks;

namespace Return.Web.Api.Services;

public interface IShareService
{
    //assumption is that shares change, should be able to get a cost for a certain point in time + current cost. Implementation should
    // be some api either public or pay to use
    Task<decimal> GetShareCostAsync(string shareId, DateTime asOfDate, CancellationToken ct = default);

    Task<decimal> GetCurrentShareCostAsync(string shareId, CancellationToken ct = default);
}