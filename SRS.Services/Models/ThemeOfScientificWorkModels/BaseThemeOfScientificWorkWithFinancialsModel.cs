using System.Collections.Generic;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class BaseThemeOfScientificWorkWithFinancialsModel : BaseThemeOfScientificWorkModel
    {
        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }

        public IList<ThemeOfScientificWorkCathedraModel> ThemeOfScientificWorkCathedras { get; set; }

        public IList<ThemeOfScientificWorkSupervisorModel> ThemeOfScientificWorkSupervisors { get; set; }
    }
}