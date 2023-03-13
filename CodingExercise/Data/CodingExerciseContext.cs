using Microsoft.EntityFrameworkCore;
using CodingExercise.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace CodingExercise.Data
{
    public class CodingExerciseContext : DbContext
    {
        public CodingExerciseContext(DbContextOptions<CodingExerciseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Investment>().ToTable("Investments");
            modelBuilder.Entity<Stock>().ToTable("Stocks");
        }
    }
}