using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPerformance.Data.Model
{
    public partial class User
    {
        public User()
        {
            UserInvestments = new HashSet<UserInvestment>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;

        [InverseProperty(nameof(UserInvestment.User))]
        public virtual ICollection<UserInvestment> UserInvestments { get; set; }
    }
}
