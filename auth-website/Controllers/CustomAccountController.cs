using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace mywebapp.Controllers
{
    [AllowAnonymous]
    [Area("MicrosoftIdentity")]
    [Route("[area]/[controller]/[action]")]
    public class CustomAccountController : Controller
    {

        [HttpGet("{scheme?}")]
        public IActionResult SignOut([FromRoute] string scheme)
        {
            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;

            var host = Request.Host.Host;
            host = host.Replace("auth.", "");

            var properties = new AuthenticationProperties
            {
                RedirectUri = "/SignedOut"
            };
            
            HttpContext.Response.Cookies.Delete(
                Environment.GetEnvironmentVariable("PreviewCookieName") ?? string.Empty,
                new CookieOptions
                {
                    Domain = host
                });
            

            return SignOut(properties, CookieAuthenticationDefaults.AuthenticationScheme, scheme);
        }

    }
}