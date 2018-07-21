﻿using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.API.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {

        public UserInfoViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}