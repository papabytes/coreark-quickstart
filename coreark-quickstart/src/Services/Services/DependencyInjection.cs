using Microsoft.Extensions.DependencyInjection;
using Services.AccountSettingses.Services;
using Services.AccountSettingses.Services.Interfaces;
using Services.CompanySettingses.Services;
using Services.CompanySettingses.Services.Interfaces;

namespace Services
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountSettingsDomainService, AccountSettingsDomainService>();
            services.AddScoped<ICompanySettingsDomainService, CompanySettingsDomainService>();
        }
    }
}