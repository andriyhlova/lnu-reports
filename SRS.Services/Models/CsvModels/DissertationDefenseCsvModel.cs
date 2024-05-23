using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class DissertationDefenseCsvModel : BaseCsvModel
    {
        [Name("Дата захисту")]
        [Index(1)]
        [DisplayName("Дата захисту")]
        public string DefenseDate { get; set; }

        [Name("Дата подання")]
        [Index(2)]
        [DisplayName("Дата подання")]
        public string SubmissionDate { get; set; }

        [Name("Керівник")]
        [Index(3)]
        [DisplayName("Керівник")]
        public string SupervisorDescription { get; set; }

        [Name("Виконавець")]
        [Index(4)]
        [DisplayName("Виконавець")]
        public string UserDescription { get; set; }

        [Name("Тип дисертації")]
        [Index(5)]
        [DisplayName("Тип дисертації")]
        public string DissertationType { get; set; }
    }
}
