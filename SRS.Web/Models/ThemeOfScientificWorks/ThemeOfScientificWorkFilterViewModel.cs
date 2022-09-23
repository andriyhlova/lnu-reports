using System;
using SRS.Domain.Enums;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.ThemeOfScientificWorks
{
    public class ThemeOfScientificWorkFilterViewModel : BaseFilterViewModel
    {
        public Financial? Financial { get; set; }

        public ScientificWorkSubCategory? SubCategory { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? PeriodFromFrom { get; set; }

        public DateTime? PeriodFromTo { get; set; }

        public DateTime? PeriodToFrom { get; set; }

        public DateTime? PeriodToTo { get; set; }
    }
}
