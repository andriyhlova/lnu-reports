﻿using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class CathedraReportCsvModel
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

        [Name("Кафедра")]
        [Index(3)]
        [DisplayName("Кафедра")]
        public string CathedraName { get; set; }

        [Name("Керівник кафедри")]
        [Index(4)]
        [DisplayName("Керівник кафедри")]
        public string Head { get; set; }
    }
}
