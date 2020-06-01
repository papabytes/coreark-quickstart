using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickStart.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<ApplicationUserRole> Roles { get; set; }
    }
}
