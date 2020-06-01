using AutoMapper;
using Entity;
using Packages.CompanySettingses.Requests;
using Packages.CompanySettingses.Responses;

namespace Services.CompanySettingses.MappingProfiles
{
    public class CompanySettingsMappingProfile : Profile
    {
        public CompanySettingsMappingProfile()
        {
            CreateMap<CompanySettingsCreateViewModel, CompanySettings>(MemberList.Source);
            CreateMap<CompanySettings, CompanySettingsViewModel>(MemberList.Destination);
        }
    }
}