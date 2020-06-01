using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QuickStart.Data
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ICollection<ApplicationUserRole> Users { get; set; }
    }
}
