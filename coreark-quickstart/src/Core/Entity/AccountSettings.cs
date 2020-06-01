using System;
using CoreArk.Packages.Core.Interfaces;

namespace Entity
{
    public class AccountSettings : IEntityWithId, IEntityWithCompanyId
    {
        public Guid Id { get; set; }

        /// <summary>
        /// i.e.: en-us
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// User's unique identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User's Company Unique Identifier
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}