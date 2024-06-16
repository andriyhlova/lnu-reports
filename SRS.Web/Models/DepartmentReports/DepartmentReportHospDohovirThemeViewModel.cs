﻿using System.Collections.Generic;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.DepartmentReports
{
    public class DepartmentReportHospDohovirThemeViewModel
    {
        public int Id { get; set; }

        public int? HospDohovirThemeId { get; set; }// 3

        public string AllDescriptionHospDohovirTheme { get; set; }

        public string CVHospDohovirTheme { get; set; }// 4.1

        public string DefensesOfCoworkersHospDohovirTheme { get; set; }// 4.2

        public string ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }// 4.4

        public string OtherHospDohovirTheme { get; set; }// 4.5

        public IList<CheckboxListItem> PrintedPublicationHospDohovirTheme { get; set; }// 4.3
    }
}