using CoreArk.Packages.Core.Interfaces;

namespace Entity.Configuration
{
    public class ConfigurationAssemblyResolver : IConfigurationAssemblyResolver
    {
        public string GetConfigurationAssembly()
        {
            return typeof(AccountSettingsConfigurations).Assembly.FullName;
        }
    }
}