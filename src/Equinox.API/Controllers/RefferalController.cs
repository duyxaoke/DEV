using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation;

namespace Equinox.API.Controllers
{
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class RefferalController : ApiController
    {
        private readonly IRefferalAppService _RefferalAppService;

        public RefferalController(
            IRefferalAppService RefferalAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _RefferalAppService = RefferalAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("list")]
        public IActionResult Get()
        {
            return Response(_RefferalAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetById/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var RefferalViewModel = _RefferalAppService.GetById(id);

            return Response(RefferalViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetByUser/{userId:guid}")]
        public IActionResult GetByUser(Guid userId)
        {
            var RefferalViewModel = _RefferalAppService.GetByUser(userId);
            return Response(RefferalViewModel);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Post([FromBody]RefferalViewModel RefferalViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(RefferalViewModel);
            }

            _RefferalAppService.Create(RefferalViewModel);

            return Response(RefferalViewModel);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(Guid id)
        {
            _RefferalAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var RefferalHistoryData = _RefferalAppService.GetAllHistory(id);
            return Response(RefferalHistoryData);
        }
    }
}