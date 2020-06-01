using System;
using System.Runtime.Serialization;
using Packages.AccountSettingses.Requests;

namespace Packages.AccountSettingses.Responses
{
    public class AccountSettingsViewModel : AccountSettingsCreateViewModel
    {
        public Guid Id { get; set; }
    }
}