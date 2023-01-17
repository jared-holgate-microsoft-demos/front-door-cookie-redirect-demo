using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;

namespace auth_website.Pages
{
    public class LoginModel : PageModel
    {
        public string? ReturnUrl { get; set; }

        public void OnGet()
        {
            var host = Request.Host.Host;
            host = host.Replace("auth.", "");

            HttpContext.Response.Cookies.Append(
                Environment.GetEnvironmentVariable("PreviewCookieName") ?? string.Empty, 
                Environment.GetEnvironmentVariable("PreviewCookieValue") ?? string.Empty, 
                new CookieOptions
                {
                    Domain = host,
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    Secure = true
                });

            ReturnUrl = host;
        }
    }
}
