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
    public class TransactionController : ApiController
    {
        private readonly ITransactionAppService _TransactionAppService;

        public TransactionController(
            ITransactionAppService TransactionAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _TransactionAppService = TransactionAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("transaction/list")]
        public IActionResult Get()
        {
            return Response(_TransactionAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("transaction/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var TransactionViewModel = _TransactionAppService.GetById(id);

            return Response(TransactionViewModel);
        }

        [HttpPost]
        [Route("transaction/create")]
        public IActionResult Post([FromBody]TransactionViewModel TransactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionViewModel);
            }

            _TransactionAppService.Create(TransactionViewModel);

            return Response(TransactionViewModel);
        }

        [HttpPut]
        [Route("transaction/update")]
        public IActionResult Put([FromBody]TransactionViewModel TransactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionViewModel);
            }
            _TransactionAppService.Update(TransactionViewModel);

            return Response(TransactionViewModel);
        }

        [HttpDelete]
        [Route("transaction/delete")]
        public IActionResult Delete(Guid id)
        {
            _TransactionAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("transaction/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var TransactionHistoryData = _TransactionAppService.GetAllHistory(id);
            return Response(TransactionHistoryData);
        }
    }
}