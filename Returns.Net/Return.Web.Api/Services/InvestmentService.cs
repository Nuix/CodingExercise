using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Return.Web.Api.Database;
using Returns.Models;

namespace Return.Web.Api.Services;

public class InvestmentService(ILogger<InvestmentService> logger, IMapper mapper, IShareService shareService): IInvestmentService
{
    private static IReadOnlyList<Investment> DatabaseData => 
        TestData.DatabaseData
        .Select(d => new Investment(d.Id, d.UserId, d.ShareName, d.ShareCount, d.CostBasisPerShare, d.PurchaseDateTime))
        .ToList();

    public async Task<IReadOnlyList<InvestmentRes>> GetByUserId(Guid userId, CancellationToken cts = default)
    {
        var investments = DatabaseData.Where(i => i.UserId == userId).ToList();
        return await MapToInvestmentRes(investments, cts);
    }

    public async Task<IReadOnlyList<InvestmentRes>> Query(InvestmentQuery query, CancellationToken cts = default)
    {
        logger.LogInformation("{InvestmentQuery}", query);
        var queryable = DatabaseData.AsQueryable();
        //usually would use expression builder or generic, but this also works as a quick example
        if(query.UserId.HasValue)
            queryable = queryable.Where(i => i.UserId == query.UserId.Value);

        if (query.Start.HasValue)
            queryable = queryable.Skip(query.Start.Value);
        
        if(query.Amount.HasValue)
            queryable = queryable.Take(query.Amount.Value);
        
        //materialize, dbcontext has tolistasync where cts would be passed
        var items = queryable.ToList();
        return await MapToInvestmentRes(items, cts);
    }
    
    
    /// <summary>
    /// Helper to simulate getting actual current share price from a sahre service
    /// </summary>
    /// <param name="investments"></param>
    /// <param name="cts"></param>
    /// <returns></returns>
    private async Task<IReadOnlyList<InvestmentRes>> MapToInvestmentRes(IReadOnlyList<Investment> investments, CancellationToken cts)
    {
        var results = new List<InvestmentRes>();
        //usually would involce some type of caching here to avoid multiple calls for same share
        foreach (var investment in investments)
        {
            var investmentRes = mapper.Map<InvestmentRes>(investment);
            var currentPrice = await new DumbyShareService().GetCurrentShareCostAsync(investment.ShareName, cts);
            var updatedInvestmentRes = investmentRes with {
                CurrentPrice = currentPrice
            };
            results.Add(updatedInvestmentRes);
        }
        
        return results;
    }
}