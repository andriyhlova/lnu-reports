﻿using SRS.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Reports
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        public string ParticipationInGrands { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public string ApplicationForInevention { get; set; }

        public string PatentForInevention { get; set; }

        public string ReviewForTheses { get; set; }

        public string MembershipInCouncils { get; set; }

        public string Other { get; set; }

        public string ThemeOfScientificWorkDescription { get; set; }

        public bool IsSigned { get; set; }

        public bool IsConfirmed { get; set; }

        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public int? ThemeOfScientificWorkId { get; set; }

        public List<CheckboxListItem> PrintedPublication { get; set; }

        public List<CheckboxListItem> RecomendedPublication { get; set; }

        public List<CheckboxListItem> AcceptedToPrintPublication { get; set; }
    }
}