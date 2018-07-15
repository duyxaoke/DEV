using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Equinox.Infra.CrossCutting.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationRole : IdentityRole
    {
        [StringLength(250)]
        public string Description { get; set; }

    }
}