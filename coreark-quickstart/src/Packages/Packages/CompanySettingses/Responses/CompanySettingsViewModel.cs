using System;
using Packages.CompanySettingses.Requests;

namespace Packages.CompanySettingses.Responses
{
    public class CompanySettingsViewModel : CompanySettingsCreateViewModel
    {
        public Guid Id { get; set; }
    }
}