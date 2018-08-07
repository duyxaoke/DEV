using System;
using Equinox.API.Helpers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Equinox.Domain.Core.Commands;
using Equinox.Domain.Core.Enums;
using OpenIddict.Validation;
using IdentityServer4.AccessTokenValidation;

namespace Equinox.API.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class TransactionController : ApiController
    {
        private readonly ITransactionAppService _TransactionAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(
            UserManager<ApplicationUser> userManager,
            ITransactionAppService TransactionAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _TransactionAppService = TransactionAppService;
        }

        [HttpGet]
        
        [Route("list")]
        public IActionResult Get()
        {
            var result = _TransactionAppService.GetAll()
                .Select(c => new TransactionViewModel{
                Id = c.Id,
                Approve = c.Approve,
                DepWithType = c.DepWithType,
                UserId = c.UserId,
                Quantity = c.Quantity,
                IP = c.IP,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                UserName = _userManager.FindByIdAsync(c.UserId.ToString()).Result.UserName
            });
            return Response(result);
        }

        [HttpGet]
        
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var TransactionViewModel = _TransactionAppService.GetById(id);

            return Response(TransactionViewModel);
        }

        [HttpPost]
        [Route("create")]
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
        [Route("update")]
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
        public IActionResult Delete(Guid id)
        {
            _TransactionAppService.Remove(id);

            return Response();
        }

        [HttpPost]
        [Route("pageData")]
        public IActionResult Data()
        {
            var ls = _TransactionAppService.GetAll()
               .Select(c => new TransactionViewModel
               {
                   Id = c.Id,
                   Approve = c.Approve,
                   DepWithType = c.DepWithType,
                   DepWithTypeName = Helper.GetEnumDescription((DepWithType)c.DepWithType),
                   UserId = c.UserId,
                   Quantity = c.Quantity,
                   IP = c.IP,
                   CreatedDate = c.CreatedDate,
                   UpdatedDate = c.UpdatedDate,
                   UserName = _userManager.FindByIdAsync(c.UserId.ToString()).Result.UserName
               });
            var parser = new Parser<TransactionViewModel>(Request.Form, ls);
            var result = parser.Parse();
            return Ok(result);
        }

        [HttpGet]
        
        [Route("history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var TransactionHistoryData = _TransactionAppService.GetAllHistory(id);
            return Response(TransactionHistoryData);
        }
    }
}