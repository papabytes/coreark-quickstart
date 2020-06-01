using System.ComponentModel.DataAnnotations;

namespace Packages.CompanySettingses.Requests
{
    public class CompanySettingsCreateViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Culture { get; set; }
    }
}