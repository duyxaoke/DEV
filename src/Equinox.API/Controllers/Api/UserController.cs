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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equinox.API.Controllers.Api
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    public class UserController : ApiController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
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
            var result = _userManager.Users.ToList();
            return Response(result);
        }

        [HttpGet]
        [Route("getRoles")]
        public async Task<IActionResult> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var assignedRoles = await _userManager.GetRolesAsync(user);

            var allRoles = await _roleManager.Roles.ToListAsync();

            var userRoles = allRoles.Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name,
                Selected = assignedRoles.Contains(r.Name),
            }).ToList();

            var viewModel = new UserRolesViewModel
            {
                Username = user.UserName,
                UserId = user.Id,
                UserRoles = userRoles,
                SelectedRoles = userRoles.Where(c=> c.Selected).Select(c=> c.Value).ToList()
            };
            return Response(viewModel);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return Response(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostAsync([FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            ApplicationUser user = new ApplicationUser
            {
                Name = model.Name,
                Balance = 0,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return Response(model);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> PutAsync([FromBody]UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Name = model.Name;
                user.Email = model.Email;
                user.UserName = model.UserName;
                if (!String.IsNullOrEmpty(model.Password))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                }
                var result = await _userManager.UpdateAsync(user);
            }
            return Response(model);
        }

        [HttpPut]
        [Route("editRoles")]
        public async Task<IActionResult> EditRoles([FromBody]UserRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            var possibleRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var submittedRoles = model.SelectedRoles;

            var shouldUpdateSecurityStamp = false;

            foreach (var submittedRole in submittedRoles)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, submittedRole);
                if (!hasRole)
                {
                    shouldUpdateSecurityStamp = true;
                    await _userManager.AddToRoleAsync(user, submittedRole);
                }
            }

            foreach (var removedRole in possibleRoles.Select(r => r.Name).Except(submittedRoles))
            {
                shouldUpdateSecurityStamp = true;
                await _userManager.RemoveFromRoleAsync(user, removedRole);
            }

            if (shouldUpdateSecurityStamp)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }
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
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser != null)
            {
                var result = await _userManager.DeleteAsync(applicationUser);
            }
            return Response();
        }

        [HttpPost]
        [Route("pageData")]
        public IActionResult Data()
        {
            var ls = _userManager.Users.ToList();
            var parser = new Parser<ApplicationUser>(Request.Form, ls);
            var result = parser.Parse();
            return Ok(result);
        }
    }
}