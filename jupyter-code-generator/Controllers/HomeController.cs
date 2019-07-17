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
using System.Diagnostics;
using jupyter_code_generator.Repositories;

namespace jupyter_code_generator.Controllers
{
    public class HomeController : Controller
    {

        private DataApiRepo _dataApiRepo;
        public HomeController(DataApiRepo dataApiRepoInjection)
        {
            this._dataApiRepo = dataApiRepoInjection;
        }

        public IActionResult Index()
        {      
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Container()
        {
            ISession clientSession = HttpContext.Session;
            if (SessionStateInvalid(clientSession))
            {
                clientSession.Clear();
                clientSession.SetString("state", Startup.serverState);
                return RedirectToAction("signIn", "Session");
            }

            var userId = HttpContext.User.Claims.FirstOrDefault(itt => itt.Type == "userId").Value;
            string accessToken = Startup.userTokenCache[userId];

            List<Container> containers = await _dataApiRepo.GetContainers(accessToken);
            containers.ForEach(c =>
           {
               Debug.WriteLine(c.ToString());
           });
            ViewData["containers"] = containers;
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
