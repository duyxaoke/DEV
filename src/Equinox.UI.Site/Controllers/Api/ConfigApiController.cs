using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Admin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ConfigApiController : ApiController
    {
        private readonly IConfigAppService _ConfigAppService;

        public ConfigApiController(
            IConfigAppService ConfigAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _ConfigAppService = ConfigAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(_ConfigAppService.GetAll());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var ConfigViewModel = _ConfigAppService.GetById(id);

            return Response(ConfigViewModel);
        }

        [HttpPost]
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
        public IActionResult Delete(Guid id)
        {
            _ConfigAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [Route("/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var ConfigHistoryData = _ConfigAppService.GetAllHistory(id);
            return Response(ConfigHistoryData);
        }
    }
}