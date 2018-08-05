using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public decimal? Balance { get; set; }
        public string Email { get; set; }
    }
}