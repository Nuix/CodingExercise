using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InvestmentAPINuix_JBF.Models;

public partial class InvestmentAPIDatabase : DbContext
{
    public InvestmentAPIDatabase()
    {
    }

    public InvestmentAPIDatabase(DbContextOptions<InvestmentAPIDatabase> options)
        : base(options)
    {
    }

    public virtual DbSet<Investment> Investments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Investment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__investme__3213E83F8393C766");

            entity.ToTable("investments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Company)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("company");
            entity.Property(e => e.CostBasisPerShare)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("cost_basis_per_share");
            entity.Property(e => e.CurrentPrice)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("current_price");
            entity.Property(e => e.CurrentValue)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("current_value");
            entity.Property(e => e.DatePurchased).HasColumnName("date_purchased");
            entity.Property(e => e.Investor).HasColumnName("investor");
            entity.Property(e => e.NumberOfShares)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("number_of_shares");
            entity.Property(e => e.StartingValue)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("starting_value");
            entity.Property(e => e.Term)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("term");
            entity.Property(e => e.TotalValue)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("total_value");

            entity.HasOne(d => d.InvestorNavigation).WithMany(p => p.Investments)
                .HasForeignKey(d => d.Investor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_investments");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F0FF7BCFB");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
