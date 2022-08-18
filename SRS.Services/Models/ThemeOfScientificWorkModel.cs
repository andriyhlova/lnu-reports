using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models
{
    public class ThemeOfScientificWorkModel : BaseThemeOfScientificWorkModel
    {
        public ScientificWorkSubCategory? SubCategory { get; set; }

        public string UserId { get; set; }

        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }
    }
}
