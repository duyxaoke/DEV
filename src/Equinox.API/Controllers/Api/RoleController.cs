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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinox.API.Controllers.Api
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    public class RoleController : ApiController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        
        [Route("list")]
        public IActionResult Get()
        {
            var result = _roleManager.Roles.ToList();
            return Response(result);
        }

        [HttpGet]
        
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return Response(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostAsync([FromBody]ApplicationRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            ApplicationRole applicationRole = new ApplicationRole();
            applicationRole.Name = model.Name;
            applicationRole.Description = model.Description;
            IdentityResult roleRuslt =  await _roleManager.CreateAsync(applicationRole);
            return Response(model);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> PutAsync([FromBody]ApplicationRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.Id);
            applicationRole.Name = model.Name;
            applicationRole.Description = model.Description;
            IdentityResult roleRuslt = await _roleManager.UpdateAsync(applicationRole);
            return Response(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
            if (applicationRole != null)
            {
                IdentityResult roleRuslt = _roleManager.DeleteAsync(applicationRole).Result;
            }
            return Response();
        }

        [HttpPost]
        [Route("pageData")]
        public IActionResult Data()
        {
            var ls = _roleManager.Roles.ToList();
            var parser = new Parser<ApplicationRole>(Request.Form, ls);
            var result = parser.Parse();
            return Ok(result);
        }
    }
}