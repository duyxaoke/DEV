using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ApplicationRoleListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfUsers { get; set; }
    }
}