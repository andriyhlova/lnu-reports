using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class BaseUserInfoCsvModel
    {
        [Name("Електронна пошта")]
        [Index(0)]
        public string Email { get; set; }

        [Name("Прізвище")]
        [Index(1)]
        public string LastName { get; set; }

        [Name("Ім'я")]
        [Index(2)]
        public string FirstName { get; set; }

        [Name("Активний")]
        [Index(3)]
        public string IsActive { get; set; }

        [Name("Ролі")]
        [Index(4)]
        public string Roles { get; set; }
    }
}
