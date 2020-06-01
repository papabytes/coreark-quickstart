using System;
using CoreArk.Packages.Core.Interfaces;

namespace Entity
{
    public class CompanySettings : IEntityWithId, IEntityWithCompanyId
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public string Culture { get; set; }
    }
}