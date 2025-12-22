using InvestmentPerformanceWebApi.DbInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformanceWebApi.DbInfrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Investment> Investments => Set<Investment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(t => t.Id);


            modelBuilder.Entity<Investment>(investment =>
            {
                investment.HasKey(t => t.Id);

                investment.Property(i => i.Id)
                    .ValueGeneratedNever();

                investment.HasOne(t => t.User)
                    .WithMany(u => u.Investments)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();

                investment.HasIndex(i => i.UserId);

            });

            var user1 = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "becho",
                    FirstName = "Echo",
                    LastName = "Bazaz",
                    CreatedUtc = DateTime.UtcNow,
                    LastUpdatedUtc = DateTime.UtcNow
                }
                );

            modelBuilder.Entity<Investment>().HasData(
               new Investment
               {
                   Id = 1,
                   UserId = 1,
                   Name = "Stock A",
                   Shares = 20,
                   CostBasis = 11.75m,
                   CurrentPrice = 80.90m,
                   PurchasedAtUtc = DateTime.UtcNow.AddMonths(-6),
                   CreatedAtUtc = DateTime.UtcNow
               },
               new Investment
               {
                   Id = 2,
                   UserId = 1,
                   Name = "Stock B",
                   Shares = 8,
                   CostBasis = 50.00m,
                   CurrentPrice = 45.69m,
                   PurchasedAtUtc = DateTime.UtcNow.AddDays(-3),
                   CreatedAtUtc = DateTime.UtcNow
               },
               new Investment
               {
                   Id = 3,
                   UserId = 1,
                   Name = "Stock C",
                   Shares = 2,
                   CostBasis = 50.00m,
                   CurrentPrice = 250.00m,
                   PurchasedAtUtc = DateTime.UtcNow.AddYears(-10),
                   CreatedAtUtc = DateTime.UtcNow
               }
           );
        }


    }
}
