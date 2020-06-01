using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuickStart.Views.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            Redirect("/Account/Login");
        }
    }
}