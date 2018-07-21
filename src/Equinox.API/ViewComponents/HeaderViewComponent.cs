using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.API.ViewComponents
{
    [Authorize]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly UserManager<ApplicationUser> _userManager;
        public HeaderViewComponent(UserManager<ApplicationUser> userManager, INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}