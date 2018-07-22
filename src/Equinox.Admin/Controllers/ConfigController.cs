using System;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Admin.Controllers
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
        public IActionResult t()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            ConfigViewModel model = new ConfigViewModel();
            return PartialView("_Add", model);
        }
        public IActionResult Edit(Guid id)
        {
            ConfigViewModel model = new ConfigViewModel();
            if (id != Guid.Empty)
            {
                model = _ConfigAppService.GetById(id);
            }
            return PartialView("_Edit", model);
        }
        public IActionResult Delete(Guid id)
        {
            ConfigViewModel model = new ConfigViewModel();
            if (id != Guid.Empty)
            {
                model = _ConfigAppService.GetById(id);
            }
            return PartialView("_Delete", model);
        }
    }
}