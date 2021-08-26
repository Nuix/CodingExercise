using Microsoft.EntityFrameworkCore;
using Nuix.Api.Data.Entities;

namespace Nuix.Api.Data.Contexts
{
  public interface INuixContext
  {
    DbSet<Investment> Investments { get; set; }
    DbSet<User> Users { get; set; }
  }
}