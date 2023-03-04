using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmentPerformance.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

    }
}

