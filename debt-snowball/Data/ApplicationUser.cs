using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace debt_snowball.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public ICollection<Debt> Debts { get; set; }
        public ICollection<Snowballa> Snowballas { get; set; }

    }
}
