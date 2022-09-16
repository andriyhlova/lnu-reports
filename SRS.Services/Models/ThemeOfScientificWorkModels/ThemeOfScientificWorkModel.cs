using System.Collections.Generic;
using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkModel : BaseThemeOfScientificWorkModel
    {
        public string OtherProjectType { get; set; }

        public ScientificWorkSubCategory? SubCategory { get; set; }

        public string UserId { get; set; }

        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }
    }
}
