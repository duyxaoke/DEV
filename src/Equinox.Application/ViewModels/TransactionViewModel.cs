using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class TransactionViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The UserId is Required")]
        [DisplayName("UserId")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The DepWithType is Required")]
        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DisplayName("DepWithType")]
        public int DepWithType { get; set; }

        [Required(ErrorMessage = "The Quantity is Required")]
        [Range(0, 999999999, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }

        public string IP { get; set; }
        public bool Approve { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}