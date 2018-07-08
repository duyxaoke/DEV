using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.WebApi.Controllers
{
    [Authorize]
    public class ConfigController : ApiController
    {
        private readonly IConfigAppService _ConfigAppService;

        public ConfigController(
            IConfigAppService ConfigAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _ConfigAppService = ConfigAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("config-management")]
        public IActionResult Get()
        {
            return Response(_ConfigAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("config-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var ConfigViewModel = _ConfigAppService.GetById(id);

            return Response(ConfigViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteConfigData")]
        [Route("config-management")]
        public IActionResult Post([FromBody]ConfigViewModel ConfigViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(ConfigViewModel);
            }

            _ConfigAppService.Create(ConfigViewModel);

            return Response(ConfigViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "CanWriteConfigData")]
        [Route("config-management")]
        public IActionResult Put([FromBody]ConfigViewModel ConfigViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(ConfigViewModel);
            }

            _ConfigAppService.Update(ConfigViewModel);

            return Response(ConfigViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveConfigData")]
        [Route("Config-management")]
        public IActionResult Delete(Guid id)
        {
            _ConfigAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Config-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var ConfigHistoryData = _ConfigAppService.GetAllHistory(id);
            return Response(ConfigHistoryData);
        }
    }
}