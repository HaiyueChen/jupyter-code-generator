using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jupyter_code_generator.Controllers
{
    public class SessionController : Controller
    {
        public SessionController(IOptions<AzureAdB2COptions> b2cOptions)
        {
            AzureAdB2COptions = b2cOptions.Value;
        }

        public AzureAdB2COptions AzureAdB2COptions { get; set; }

        [HttpGet]
        public IActionResult signIn()
        {
            var redirectUrl = Url.Action(nameof(HomeController.Index), "Home");
            HttpContext.Session.SetString("state", Startup.serverState);
            return Challenge(
                        new AuthenticationProperties
                        {
                            RedirectUri = redirectUrl
                        },
                        OpenIdConnectDefaults.AuthenticationScheme
                    );
        }

        [HttpGet]
        public IActionResult signOut()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            Startup.userTokenCache.Remove(userId);

            var callbackUrl = Url.Action(nameof(SignedOut), "Session", values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = callbackUrl
                },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme
                );
        }


        [HttpGet]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }
    }
}
