using System.ComponentModel.DataAnnotations;

namespace Equinox.Infra.CrossCutting.Identity.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
