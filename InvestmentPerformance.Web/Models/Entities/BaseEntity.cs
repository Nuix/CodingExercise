using System;

namespace InvestmentPerformance.Web.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        
        public DateTimeOffset CreatedDateUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastUpdateDateUtc { get; set; } = DateTimeOffset.UtcNow;
    }
}