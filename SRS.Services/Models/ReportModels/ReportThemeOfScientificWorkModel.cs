﻿using SRS.Services.Models.BaseModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportThemeOfScientificWorkModel : BaseModel
    {
        public int ThemeOfScientificWorkId { get; set; }

        public ThemeOfScientificWorkModel ThemeOfScientificWork { get; set; }

        public string Description { get; set; }
    }
}