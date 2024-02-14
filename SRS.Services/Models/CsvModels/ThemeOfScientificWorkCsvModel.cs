using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class ThemeOfScientificWorkCsvModel : BaseCsvModel
    {
        [Name("Номер державної реєстрації")]
        [Index(1)]
        public string ThemeNumber { get; set; }

        [Name("Внутрішній шифр")]
        [Index(2)]
        public string Code { get; set; }

        [Name("Науковий керівник (нове поле)")]
        [Index(3)]
        public string SupervisorDescription { get; set; }

        [Name("Факультети")]
        [Index(4)]
        public string Faculties { get; set; }

        [Name("Кафедри")]
        [Index(5)]
        public string Cathedras { get; set; }

        [Name("Період виконання початок")]
        [Index(6)]
        public string PeriodFrom { get; set; }

        [Name("Період виконання кінець")]
        [Index(7)]
        public string PeriodTo { get; set; }

        [Name("Тип проєкту")]
        [Index(8)]
        public string Financial { get; set; }

        [Name("Підкатегорія")]
        [Index(9)]
        public string SubCategory { get; set; }

        [Name("План")]
        [Index(10)]
        public decimal? PlannedAmount { get; set; }

        [Name("Валюта")]
        [Index(11)]
        public string Currency { get; set; }

        [Name("Фінансування")]
        [Index(12)]
        public string Amount { get; set; }
    }
}
