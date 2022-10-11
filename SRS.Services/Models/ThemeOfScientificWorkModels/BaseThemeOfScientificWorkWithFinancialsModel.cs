using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class BaseThemeOfScientificWorkWithFinancialsModel : BaseThemeOfScientificWorkModel
    {
        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }
    }
}
