using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Equinox.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(HttpClient client)
        {
            _client = client;
        }


        public async Task CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:50001/api/Test/list");
        }


        [HttpGet("~/")]
        public ActionResult Index()
        {
            return View("Home");
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

    }
}
