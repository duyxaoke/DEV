using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Core.Bus;
using Equinox.Infra.CrossCutting.Identity.Models;

namespace Equinox.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public HomeController(RoleManager<ApplicationRole> roleManager,
                                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> AddRole()
        {
            ApplicationRole applicationRole = new ApplicationRole();
            applicationRole.Name = "Administrator";
            applicationRole.Description = "Administrator";
            IdentityResult roleRuslt = await _roleManager.CreateAsync(applicationRole);


            var user = await _userManager.GetUserAsync(User);
            var claim = new Claim("role", "Administrator");
            var addClaimResult = await _userManager.AddClaimAsync(user, claim);
            return View(addClaimResult);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
