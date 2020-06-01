using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickStart.Data
{
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
