using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class BaseCsvModel
    {
        [Name("Назва")]
        [Index(0)]
        public string Name { get; set; }
    }
}
