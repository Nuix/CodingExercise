using System;
using System.Collections.Generic;

namespace Returns.Models;

public static class TestData
{
    //temp, only needed due to test data here, but also being used in api. 
    public static string Stock1Name = "Stock1";
    public static string Stock2Name = "Stock2";
    public static string Stock3Name = "Stock3";
    
    public static Guid User1Id = Guid.Parse("a8b9c0d1-e2f3-4345-a678-b9c0d1e2f345");
    public static Guid User2Id = Guid.Parse("b9c0d1e2-f3a4-4456-b789-c0d1e2f3a456");
    public static Guid User3Id = Guid.Parse("c0d1e2f3-a4b5-4567-c890-d1e2f3a4b567");
    
    
    public record InvestmentData(
        Guid Id,
        Guid UserId,
        string ShareName,
        decimal ShareCount,
        decimal CostBasisPerShare,
        DateTime PurchaseDateTime);
    
    public static IReadOnlyList<InvestmentData> DatabaseData = [
        new(Guid.NewGuid(), User1Id, Stock1Name, 5, 10, new(2023, 1, 1)),
        new(Guid.NewGuid(), User1Id, Stock2Name, 10, 20, new(2023, 6, 1)),
        new(Guid.NewGuid(), User2Id, Stock2Name, 15, 30, new(2024, 1, 1)),
        new(Guid.NewGuid(), User2Id, Stock3Name, 20, 40, new(2022, 1, 1)),
    ];
}

