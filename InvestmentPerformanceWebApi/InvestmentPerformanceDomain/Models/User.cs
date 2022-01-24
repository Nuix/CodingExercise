using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPerformanceDomain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class UserEntityTypeConfguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(1000);
    }
}