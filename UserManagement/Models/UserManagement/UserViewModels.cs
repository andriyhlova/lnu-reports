using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using UserManagement.Models.db;

namespace UserManagement.Models
{
    public class UserUpdateViewModel
    {
        [Required]
        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }
    }
}