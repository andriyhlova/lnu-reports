using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class CathedraCsvModel : BaseCsvModel
    {
        [Name("Назва факультету")]
        [Index(1)]
        public string FacultyName { get; set; }
    }
}
