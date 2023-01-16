using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;

namespace auth_website.Pages
{
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            var hosts = HttpContext.Request.Headers["X-Forwarded-Host"];
            var host = string.Empty;
            if(hosts.Count != 0)
            {
                host = hosts[0] ?? string.Empty;
            }
            host = host.Replace("auth.", "");

            HttpContext.Response.Cookies.Append("_previewID", "213c5104-0bef-4c61-9da2-2d634931e3d7", new CookieOptions
            {
                Domain = host,
                
            });

            ReturnUrl = host;
        }
    }
}
