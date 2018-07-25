using System;
using Equinox.API.Helpers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation;

namespace Equinox.API.Controllers.Api
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    public class CurrencyController : ApiController
    {
        private readonly ICurrencyAppService _CurrencyAppService;

        public CurrencyController(
            ICurrencyAppService CurrencyAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _CurrencyAppService = CurrencyAppService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult Get()
        {
            return Response(_CurrencyAppService.GetAll());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var CurrencyViewModel = _CurrencyAppService.GetById(id);

            return Response(CurrencyViewModel);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Post([FromBody]CurrencyViewModel CurrencyViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CurrencyViewModel);
            }

            _CurrencyAppService.Create(CurrencyViewModel);

            return Response(CurrencyViewModel);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Put([FromBody]CurrencyViewModel CurrencyViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CurrencyViewModel);
            }

            _CurrencyAppService.Update(CurrencyViewModel);

            return Response(CurrencyViewModel);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _CurrencyAppService.Remove(id);

            return Response();
        }
        [HttpPost]
        [Route("pageData")]
        public IActionResult Data()
        {
            var parser = new Parser<CurrencyViewModel>(Request.Form, _CurrencyAppService.GetAll());
            var result = parser.Parse();
            return Ok(result);
        }
        [HttpGet]
        [Route("history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var CurrencyHistoryData = _CurrencyAppService.GetAllHistory(id);
            return Response(CurrencyHistoryData);
        }
    }
}