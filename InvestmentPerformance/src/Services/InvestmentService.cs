using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentPerformance.Api.Models;

namespace InvestmentPerformance.Api.Services;

public class InvestmentService : IInvestmentService
{
    private readonly List<InvestmentModel> _data =
    [
        new(1, 101, "Ford Motor Company", 3, 13.25m, 13.25m, DateTime.UtcNow),
        new(2, 102, "Nvidia Corporation", 5, 102.67m, 189.06m, DateTime.UtcNow.AddDays(-10)),
        new(3, 102, "SoFi Technologies", 10, 10.50m, 28.38m, DateTime.UtcNow.AddYears(-2)),
        new(4, 102, "SoFi Technologies", 2.5m, 28.38m, 28.38m, DateTime.UtcNow)
    ];

    public IEnumerable<UserInvestmentRecord> GetInvestmentsByUserId(int userId) =>
        _data.Where(d => d.UserId == userId).Select(i => new UserInvestmentRecord(i.Id, i.Name));

    public InvestmentRecord? GetInvestmentById(int investmentId) => _data.FirstOrDefault(i => i.Id == investmentId)?.ToInvestment();
}