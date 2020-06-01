using AutoMapper;
using Entity;
using Packages.AccountSettingses.Requests;
using Packages.AccountSettingses.Responses;

namespace Services.AccountSettingses.MappingProfiles
{
    public class AccountSettingsMappingProfile : Profile
    {
        public AccountSettingsMappingProfile()
        {
            CreateMap<AccountSettingsCreateViewModel, AccountSettings>(MemberList.Source);
            CreateMap<AccountSettings, AccountSettingsViewModel>(MemberList.Destination);
        }
    }
}