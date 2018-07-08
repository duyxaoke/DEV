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
        [AllowAnonymous]
        [Route("currency/list")]
        public IActionResult Get()
        {
            return Response(_CurrencyAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("currency/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var CurrencyViewModel = _CurrencyAppService.GetById(id);

            return Response(CurrencyViewModel);
        }

        [HttpPost]
        [Route("currency/create")]
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
        [Route("currency/update")]
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
        [Route("currency/delete")]
        public IActionResult Delete(Guid id)
        {
            _CurrencyAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("currency/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var CurrencyHistoryData = _CurrencyAppService.GetAllHistory(id);
            return Response(CurrencyHistoryData);
        }
    }
}