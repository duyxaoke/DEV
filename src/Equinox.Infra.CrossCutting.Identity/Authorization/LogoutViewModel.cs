using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Equinox.Infra.CrossCutting.Identity.Authorization
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
