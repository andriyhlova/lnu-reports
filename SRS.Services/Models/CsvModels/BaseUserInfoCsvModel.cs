using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class BaseUserInfoCsvModel
    {
        [Name("Електронна пошта")]
        [Index(0)]
        [DisplayName("Електронна пошта")]
        public string Email { get; set; }

        [Name("Прізвище")]
        [Index(1)]
        [DisplayName("Прізвище")]
        public string LastName { get; set; }

        [Name("Ім'я")]
        [Index(2)]
        [DisplayName("Ім'я")]
        public string FirstName { get; set; }

        [Name("Активний")]
        [Index(3)]
        [DisplayName("Активний")]
        public string IsActive { get; set; }

        [Name("Ролі")]
        [Index(4)]
        [DisplayName("Ролі")]
        public string Roles { get; set; }
    }
}
