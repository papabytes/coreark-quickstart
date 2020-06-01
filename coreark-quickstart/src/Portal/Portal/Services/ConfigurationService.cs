

using Microsoft.Extensions.Configuration;

namespace Portal.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiBaseAddress()
        {
            return _configuration.GetValue<string>("ApiBaseAddress");
        }

        public string GetIdentityBaseAddress()
        {
            return _configuration.GetValue<string>("IdentityAddress");
        }
    }
}