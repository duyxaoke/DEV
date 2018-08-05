using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Commands;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equinox.Admin.Controllers
{
    [Authorize]
    public class RolesController : BaseController
    {
        public RolesController(INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
