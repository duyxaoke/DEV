using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Equinox.Admin.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;

        public BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = HttpContext.GetTokenAsync("access_token").Result;
                ViewBag.Token = accessToken;
            }
        }
        public bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

    }
}
