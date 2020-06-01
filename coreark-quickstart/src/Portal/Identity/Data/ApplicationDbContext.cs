using System.Collections.Generic;
using CoreArk.Packages.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuickStart.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(c =>
            {
                c.HasKey(e => e.Id);
                c.Property(e => e.Name)
                    .IsRequired();
                c.HasMany(e => e.Users)
                    .WithOne(rl => rl.Company)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<ApplicationUser>(au =>
            {
                au.HasKey(e => e.Id);
                au.Property(e => e.FirstName)
                    .IsRequired();
                au.Property(e => e.LastName)
                    .IsRequired();

                au.HasOne(e => e.Company)
                    .WithMany(rl => rl.Users)
                    .HasForeignKey(e => e.CompanyId);

                au.HasMany(e => e.Roles)
                    .WithOne(rl => rl.User)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ApplicationUserRole>(aur =>
            {
                aur.HasOne(e => e.Role)
                    .WithMany(rl => rl.Users)
                    .HasForeignKey(e => e.RoleId);

                aur.HasOne(e => e.User)
                    .WithMany(rl => rl.Roles)
                    .HasForeignKey(e => e.UserId);
            });

            builder.Entity<ApplicationRole>(ar =>
            {
                ar.HasMany(e => e.Users)
                    .WithOne(rl => rl.Role)
                    .OnDelete(DeleteBehavior.Cascade);

                ar.HasData(new List<ApplicationRole>
                {
                    new ApplicationRole
                    {
                        Id = "acd59a30-5020-4c7d-8421-82e5855bf230",
                        Name = UserRole.User.ToString(),
                        NormalizedName = UserRole.User.ToString().ToUpperInvariant()
                    },
                    new ApplicationRole
                    {
                        Id = "acd59a30-5020-4c7d-8421-82e5855bf231",
                        Name = UserRole.Support.ToString(),
                        NormalizedName = UserRole.Support.ToString().ToUpperInvariant()
                    },
                    new ApplicationRole
                    {
                        Id = "acd59a30-5020-4c7d-8421-82e5855bf232",
                        Name = UserRole.Administrator.ToString(),
                        NormalizedName = UserRole.Administrator.ToString().ToUpperInvariant()
                    },
                    new ApplicationRole
                    {
                        Id = "acd59a30-5020-4c7d-8421-82e5855bf233",
                        Name = UserRole.Sysadmin.ToString(),
                        NormalizedName = UserRole.Sysadmin.ToString().ToUpperInvariant()
                    },
                });
            });
        }
    }
}