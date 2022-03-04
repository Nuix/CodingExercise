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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Shane\\source\\repos\\CodingExercise\\InvestmentPerformance\\InvestmentPerformance.Data\\InvestmentPerformanceDatabase.mdf;Integrated Security=True");
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
