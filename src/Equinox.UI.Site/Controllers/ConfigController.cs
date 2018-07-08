using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Admin.Controllers
{
    [Authorize]
    public class ConfigController : BaseController
    {
        private readonly IConfigAppService _ConfigAppService;

        public ConfigController(
            IConfigAppService ConfigAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _ConfigAppService = ConfigAppService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}