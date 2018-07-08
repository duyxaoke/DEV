using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class CurrencyViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Code is Required")]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [MaxLength(100)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Range(0, 999999999, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }

        [Required(ErrorMessage = "The IsActive is Required")]
        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
    }
}