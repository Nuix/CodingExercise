using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Return.Web.Api.Services;

//represents some api service. should include caching to not request current price based every call
public class DumbyShareService: IShareService
{
    public async Task<decimal> GetShareCostAsync(string name, DateTime asOfDate, CancellationToken ct = default)
    {
        return name switch {
            "Stock1" => 100.00m,
            "Stock2" => 50.00m,
            "Stock3" => 25.00m,
            _ => throw new ArgumentOutOfRangeException(nameof(name), $"No share cost data for share '{name}'")
        };
    }
    

    public async Task<decimal> GetCurrentShareCostAsync(string shareId, CancellationToken ct = default) 
        => await GetShareCostAsync(shareId, DateTime.UtcNow, ct);
}