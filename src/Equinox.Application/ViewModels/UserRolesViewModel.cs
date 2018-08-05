using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {
            UserRoles = new List<SelectListItem>();
            SelectedRoles = new List<String>();
        }

        public String UserId { get; set; }
        public String Username { get; set; }
        public List<SelectListItem> UserRoles { get; set; }
        public List<String> SelectedRoles { get; set; }
    }
}