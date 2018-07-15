using System.ComponentModel.DataAnnotations;

namespace Equinox.Infra.CrossCutting.Identity.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
