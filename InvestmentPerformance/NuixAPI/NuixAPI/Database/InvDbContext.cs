using Microsoft.EntityFrameworkCore;
using NuixAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuixAPI.Database
{
    public class InvDbContext : DbContext
    {
        public InvDbContext(DbContextOptions<InvDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
    }
}
