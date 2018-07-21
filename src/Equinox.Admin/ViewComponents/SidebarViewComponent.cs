using System.Threading.Tasks;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Admin.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {

        public SidebarViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}