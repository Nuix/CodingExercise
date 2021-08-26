using Microsoft.EntityFrameworkCore;
using Nuix.Api.Data.Entities;

namespace Nuix.Api.Data.Contexts
{
  public partial class NuixContext : DbContext, INuixContext
  {
    public NuixContext() { }

    public NuixContext(DbContextOptions<NuixContext> options) : base(options) { }

    public virtual DbSet<Investment> Investments { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Investment>(entity =>
      {
        entity.ToTable("Investment");

        entity.Property(e => e.CostBasis).HasColumnType("money");
        entity.Property(e => e.PurchasedUtc).HasColumnName("PurchasedUTC");
        entity.Property(e => e.Name)
          .HasMaxLength(100)
          .IsUnicode(true);
        entity.Property(e => e.Symbol)
          .IsRequired()
          .HasMaxLength(5)
          .IsUnicode(false);
        entity.HasOne(d => d.User)
        .WithMany(p => p.Investments)
        .HasForeignKey(d => d.UserId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Investment_InvestmentId");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.ToTable("User");
        entity.Property(e => e.Email).HasMaxLength(255);
        entity.Property(e => e.FirstName).HasMaxLength(255);
        entity.Property(e => e.LastName).HasMaxLength(255);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}