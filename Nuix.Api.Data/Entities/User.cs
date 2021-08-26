using System.Collections.Generic;

namespace Nuix.Api.Data.Entities
{
  public partial class User
  {
    public User()
    {
      Investments = new HashSet<Investment>();
    }

    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public virtual ICollection<Investment> Investments { get; set; }
  }
}