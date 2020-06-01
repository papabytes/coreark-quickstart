using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Configuration
{
    public class AccountSettingsConfigurations : IEntityTypeConfiguration<AccountSettings>
    {
        public void Configure(EntityTypeBuilder<AccountSettings> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).IsUnique();
        }
    }
}