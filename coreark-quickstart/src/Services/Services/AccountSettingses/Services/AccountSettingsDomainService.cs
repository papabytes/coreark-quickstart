using System.Linq;
using AutoMapper;
using CoreArk.Packages.Common.Enums;
using CoreArk.Packages.Core;
using CoreArk.Packages.Services;
using Entity;
using Services.AccountSettingses.Services.Interfaces;

namespace Services.AccountSettingses.Services
{
    public class AccountSettingsDomainService : BaseDomainService<AccountSettings, DataContext>,
        IAccountSettingsDomainService
    {
        public AccountSettingsDomainService(DataContext dbContext, IMapper mapper, IContextService contextService) :
            base(dbContext, mapper, contextService)
        {
        }

        protected override IQueryable<AccountSettings> FilterQuery(IQueryable<AccountSettings> query)
        {
            return ContextService.UserRole == UserRole.Sysadmin
                ? query
                : query.Where(settings => settings.UserId == ContextService.UserId);
        }
    }
}