using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;
using Equinox.Infra.CrossCutting.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Admin.ViewComponents
{
    [Authorize]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notifications;
        public HeaderViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}