using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Admin.ViewComponents
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