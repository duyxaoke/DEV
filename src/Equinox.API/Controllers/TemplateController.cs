using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Equinox.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenIddict.Abstractions;
using OpenIddict.Validation;

namespace Equinox.API.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Header()
        {
            return PartialView();
        }
        public IActionResult Sidebar()
        {
            return PartialView();
        }
        public IActionResult Footer()
        {
            return PartialView();
        }
    }
}
