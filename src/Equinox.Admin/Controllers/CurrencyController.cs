using System;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Admin.Controllers
{
    [Authorize]
    public class CurrencyController : BaseController
    {
        public CurrencyController(INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}