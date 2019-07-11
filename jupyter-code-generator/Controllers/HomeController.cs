using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jupyter_code_generator.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace jupyter_code_generator.Controllers
{
    public class HomeController : Controller
    {


        public HomeController(IOptions<AzureAdB2COptions> azureAdB2COptionsInjection)
        {
            this.azureAdB2COptions = azureAdB2COptionsInjection.Value;
        }

        public AzureAdB2COptions azureAdB2COptions { get; set; }


        public IActionResult Index()
        {
            var scope = azureAdB2COptions.DataApiScope;

            
            //IConfidentialClientApplication cca =
            //    ConfidentialClientApplicationBuilder.Create(azureAdB2COptions.ClientId)
            //                                        .WithRedirectUri(azureAdB2COptions.RedirectUri)
            //                                        .WithClientSecret(azureAdB2COptions.ClientSecret)
            //                                        .WithB2CAuthority(azureAdB2COptions.Authority)
            //                                        .Build();
            var cache = Startup.userTokenCache;
            
            return View();
        }


        [Authorize]
        public IActionResult Container()
        {

            return View();
        }


        [Authorize]
        public IActionResult Privacy()
        {
            ISession clientSession = HttpContext.Session;
            if (SessionStateInvalid(clientSession))
            {
                clientSession.Clear();
                clientSession.SetString("state", Startup.serverState);
                return RedirectToAction("signIn", "Session");
            }
            
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private bool SessionStateInvalid(ISession currentSession)
        {
            string clientSessionState = currentSession.GetString("state");
            return  string.IsNullOrEmpty(clientSessionState)
                    || !string.Equals(Startup.serverState, clientSessionState);
        }

    }
}
