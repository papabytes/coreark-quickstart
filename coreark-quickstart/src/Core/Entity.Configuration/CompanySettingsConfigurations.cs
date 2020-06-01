using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Configuration
{
    public class CompanySettingsConfigurations : IEntityTypeConfiguration<CompanySettings>
    {
        public void Configure(EntityTypeBuilder<CompanySettings> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.CompanyId).IsUnique();
        }
    }
}