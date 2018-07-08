using System.Collections.Generic;
using Equinox.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.WebApi.Controllers
{
    public class AppUtils
    {
        internal static IActionResult SignIn(ApplicationUser user, IList<string> roles)
        {
            var userResult = new { User = new { DisplayName = user.UserName, Roles = roles } };
            return new ObjectResult(userResult);
        }

    }
}