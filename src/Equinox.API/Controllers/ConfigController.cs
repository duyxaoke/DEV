using System;
using System.Collections.Generic;
using Equinox.API.Helpers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation;

namespace Equinox.API.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class ConfigController : ApiController
    {
        private readonly IConfigAppService _ConfigAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfigController(
            IConfigAppService ConfigAppService,
            INotificationHandler<DomainNotification> notifications, UserManager<ApplicationUser> userManager,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;

            _ConfigAppService = ConfigAppService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult List()
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
        [Route("create")]
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
        [Route("update")]
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
        [HttpPost]
        [Route("pageData")]
        public IActionResult Data()
        {
            var parser = new Parser<ConfigViewModel>(Request.Form, _ConfigAppService.GetAll());
            var result = parser.Parse();
            return Ok(result);
        }




        [HttpGet]
        [Route("history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var ConfigHistoryData = _ConfigAppService.GetAllHistory(id);
            return Response(ConfigHistoryData);
        }
    }
}