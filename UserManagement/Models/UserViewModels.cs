using SRS.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class UserUpdateViewModel
    {
        [Required]
        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }
    }
}