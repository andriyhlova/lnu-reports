using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class ReportCsvModel
    {
        [Name("Протокол")]
        [Index(0)]
        [DisplayName("Протокол")]
        public string Protocol { get; set; }

        [Name("Дата")]
        [Index(1)]
        [DisplayName("Дата")]
        public string Date { get; set; }

        [Name("Статус")]
        [Index(2)]
        [DisplayName("Статус")]
        public string State { get; set; }

        [Name("Працівник")]
        [Index(3)]
        [DisplayName("Працівник")]
        public string Employee { get; set; }
    }
}
