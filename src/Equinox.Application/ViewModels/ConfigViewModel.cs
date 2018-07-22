using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ConfigViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("SystemEnable")]
        public bool SystemEnable { get; set; }

        [Required(ErrorMessage = "The Currency is Required")]
        [MinLength(2)]
        [MaxLength(10)]
        [DisplayName("Currency")]
        public string Currency { get; set; }

        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DisplayName("ReferalBonus")]
        public decimal? ReferalBonus { get; set; }
    }
}