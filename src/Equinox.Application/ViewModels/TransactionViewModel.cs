using Equinox.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity;
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
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "The DepWithType is Required")]
        [DisplayName("DepWithType")]
        public int DepWithType { get; set; }
        public string DepWithTypeName { get; set; }

        [Required(ErrorMessage = "The Quantity is Required")]
        [Range(0, 1000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }

        public string IP { get; set; }
        public int Approve { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }

    }
}