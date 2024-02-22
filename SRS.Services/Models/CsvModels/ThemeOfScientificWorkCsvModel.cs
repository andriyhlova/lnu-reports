using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class ThemeOfScientificWorkCsvModel : BaseCsvModel
    {
        [Name("Номер державної реєстрації")]
        [Index(1)]
        [DisplayName("Номер державної реєстрації")]
        public string ThemeNumber { get; set; }

        [Name("Внутрішній шифр")]
        [Index(2)]
        [DisplayName("Внутрішній шифр")]
        public string Code { get; set; }

        [Name("Науковий керівник (нове поле)")]
        [Index(3)]
        [DisplayName("Наукові керівники")]
        public string SupervisorsDescription { get; set; }

        [Name("Факультети")]
        [Index(4)]
        [DisplayName("Факультети")]
        public string Faculties { get; set; }

        [Name("Кафедри")]
        [Index(5)]
        [DisplayName("Кафедри")]
        public string Cathedras { get; set; }

        [Name("Період виконання початок")]
        [Index(6)]
        [DisplayName("Період виконання початок")]
        public string PeriodFrom { get; set; }

        [Name("Період виконання кінець")]
        [Index(7)]
        [DisplayName("Період виконання кінець")]
        public string PeriodTo { get; set; }

        [Name("Тип проєкту")]
        [Index(8)]
        [DisplayName("Тип проєкту")]
        public string Financial { get; set; }

        [Name("Підкатегорія")]
        [Index(9)]
        [DisplayName("Підкатегорія")]
        public string SubCategory { get; set; }

        [Name("План")]
        [Index(10)]
        [DisplayName("План")]
        public decimal? PlannedAmount { get; set; }

        [Name("Валюта")]
        [Index(11)]
        [DisplayName("Валюта")]
        public string Currency { get; set; }

        [Name("Фінансування")]
        [Index(12)]
        [DisplayName("Фінансування")]
        public string Amount { get; set; }
    }
}
