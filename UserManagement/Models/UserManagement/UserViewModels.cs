using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.Models.db;

namespace UserManagement.Models
{
    public class UserUpdateViewModel
    {
        [Required]
        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }
    }
}