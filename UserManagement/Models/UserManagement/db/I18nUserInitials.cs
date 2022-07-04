using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.db
{
    public class I18nUserInitials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Language Language { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FathersName { get; set; } = "";


        public virtual ApplicationUser User { get; set; }
    }
}