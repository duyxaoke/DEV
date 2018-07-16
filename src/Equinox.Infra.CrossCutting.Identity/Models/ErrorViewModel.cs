using System.ComponentModel.DataAnnotations;

namespace Equinox.Infra.CrossCutting.Identity.Models
{
    public class ErrorViewModel
    {
        [Display(Name = "Error")]
        public string Error { get; set; }

        [Display(Name = "Description")]
        public string ErrorDescription { get; set; }
    }
}
