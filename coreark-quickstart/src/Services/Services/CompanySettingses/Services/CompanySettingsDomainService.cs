using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreArk.Packages.Common.Enums;
using CoreArk.Packages.Core;
using CoreArk.Packages.Exceptions;
using CoreArk.Packages.Services;
using Entity;
using Microsoft.EntityFrameworkCore;
using Services.CompanySettingses.Services.Interfaces;

namespace Services.CompanySettingses.Services
{
    public class CompanySettingsDomainService : BaseDomainService<CompanySettings, DataContext>,
        ICompanySettingsDomainService
    {
        public CompanySettingsDomainService(DataContext dbContext, IMapper mapper, IContextService contextService) :
            base(dbContext, mapper, contextService)
        {
        }

        public override async Task<TResponse> Create<TResponse, TRequest>(TRequest createRequest)
        {
            var exists = await Set.AsNoTracking()
                .AnyAsync(settings => settings.CompanyId == ContextService.CompanyId);
            
            if(exists) throw new BadRequestException($"Company Settings already exists for company {ContextService.CompanyId}");
            
            return await base.Create<TResponse, TRequest>(createRequest);
        }

        protected override IQueryable<CompanySettings> FilterQuery(IQueryable<CompanySettings> query)
        {
            return ContextService.UserRole == UserRole.Sysadmin
                ? query
                : query.Where(c => c.CompanyId == ContextService.CompanyId);
        }

        public override async Task<TResponse> Update<TResponse, TRequest>(Guid id, TRequest updateRequest)
        {
            var query = Set.Where(e => e.Id == id).AsNoTracking();
            query = FilterQuery(query);

            var existing = await query.FirstOrDefaultAsync();

            _ = existing ?? throw new NotFoundException(nameof(CompanySettings), id);

            Mapper.Map(updateRequest, existing);
            Set.Update(existing);
            var result = await DbContext.SaveChangesAsync();

            return Mapper.Map<TResponse>(result);
        }
    }
}