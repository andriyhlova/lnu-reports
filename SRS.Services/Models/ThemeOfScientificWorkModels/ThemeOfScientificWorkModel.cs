using System.Collections.Generic;
using SRS.Domain.Enums;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkModel : BaseThemeOfScientificWorkModel
    {
        public ScientificWorkSubCategory? SubCategory { get; set; }

        public string UserId { get; set; }

        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }
    }
}
