using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using SRS.Services.Models.UserModels;
using System.Collections.Generic;
using System.Linq;

namespace SRS.Services.Models.ReportModels
{
    public class ReportThemeOfScientificWorkModel : BaseModel
    {
        public int ThemeOfScientificWorkId { get; set; }

        public ThemeOfScientificWorkModel ThemeOfScientificWork { get; set; }

        public string Description { get; set; }

        public string Resume { get; set; }

        public string DefendedDissertation { get; set; }

        public string Publications { get; set; }

        public virtual IList<BaseUserInfoModel> ApplicationUserFullTime { get; set; }

        public virtual IList<BaseUserInfoModel> ApplicationUserExternalPartTime { get; set; }

        public virtual IList<BaseUserInfoModel> ApplicationUserLawContract { get; set; }

        public string GetApplicationUserFullTime(bool name = true)
        {
            if (name)
            {
                return string.Join(",", ApplicationUserFullTime
                             .Select(c => c.I18nUserInitials
                             .Select(x => x.Language == Language.UA ? x.LastName + " " + x.FirstName + " " + x.FathersName : string.Empty)
                             .FirstOrDefault()));
            }
            else
            {
                return string.Join(",", ApplicationUserFullTime
                             .Select(c => c.Id));
            }
        }

        public string GetApplicationUserExternalPartTime(bool name = true)
        {
            if (name)
            {
                return string.Join(",", ApplicationUserExternalPartTime
                         .Select(c => c.I18nUserInitials
                         .Select(x => x.Language == Language.UA ? x.LastName + " " + x.FirstName + " " + x.FathersName : string.Empty)
                         .FirstOrDefault()));
            }
            else
            {
                return string.Join(",", ApplicationUserExternalPartTime
                             .Select(c => c.Id));
            }
        }

        public string GetApplicationUserLawContract(bool name = true)
        {
            if (name)
            {
                return string.Join(",", ApplicationUserLawContract
                             .Select(c => c.I18nUserInitials
                             .Select(x => x.Language == Language.UA ? x.LastName + " " + x.FirstName + " " + x.FathersName : string.Empty)
                             .FirstOrDefault()));
            }
            else
            {
                return string.Join(",", ApplicationUserLawContract
                             .Select(c => c.Id));
            }
        }
    }
}
