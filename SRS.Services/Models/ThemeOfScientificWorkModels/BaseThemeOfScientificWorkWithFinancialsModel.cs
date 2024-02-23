using System.Collections.Generic;
using System.Linq;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class BaseThemeOfScientificWorkWithFinancialsModel : BaseThemeOfScientificWorkModel
    {
        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }

        public IList<ThemeOfScientificWorkCathedraModel> ThemeOfScientificWorkCathedras { get; set; }

        public IList<ThemeOfScientificWorkSupervisorModel> ThemeOfScientificWorkSupervisors { get; set; }

        public string GetSupervisors()
        {
            return string.Join(", ", ThemeOfScientificWorkSupervisors.Select(x => x.SupervisorName));
        }
    }
}