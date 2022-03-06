using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using InvestmentPerformance.Data.Model;

namespace InvestmentPerformance.Data
{
    public partial class InvestmentPerformanceContext : DbContext
    {
        public InvestmentPerformanceContext()
        {
        }

        public InvestmentPerformanceContext(DbContextOptions<InvestmentPerformanceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserInvestment> UserInvestments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var directory = Directory.GetParent(Environment.CurrentDirectory).FullName;
                var connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={directory}\\InvestmentPerformance.Data\\InvestmentPerformanceDatabase.mdf;Integrated Security=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInvestment>(entity =>
            {
                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.UserInvestments)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInvestments_Listings");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInvestments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInvestments_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
