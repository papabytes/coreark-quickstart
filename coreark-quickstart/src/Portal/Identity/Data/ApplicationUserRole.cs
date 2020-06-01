using Microsoft.AspNetCore.Identity;

namespace QuickStart.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationRole Role { get; set; }

        public ApplicationUser User { get; set; }
    }
}