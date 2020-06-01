namespace Portal.Services
{
    public interface IConfigurationService
    {
        string GetApiBaseAddress();
        string GetIdentityBaseAddress();
    }
}