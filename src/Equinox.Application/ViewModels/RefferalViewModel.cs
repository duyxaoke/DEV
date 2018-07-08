using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class RefferalViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The UserId is Required")]
        [DisplayName("UserId")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The UserRefId is Required")]
        [DisplayName("UserRefId")]
        public Guid UserRefId { get; set; }
    }
}