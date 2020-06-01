using System.ComponentModel.DataAnnotations;

namespace Packages.AccountSettingses.Requests
{
    public class AccountSettingsCreateViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Culture { get; set; }
    }
}