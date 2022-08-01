﻿using System.Linq;
using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class CathedraReportPublicationSpecification : BaseSpecification<Publication>
    {
        public CathedraReportPublicationSpecification(CathedraReportPublicationFilterModel filterModel)
            : base(
                  x => x.PrintedPublicationReport.Any(y => y.User.CathedraId == filterModel.CathedraId && y.ThemeOfScientificWork.Financial == filterModel.Financial && y.IsSigned && y.IsConfirmed),
                  true)
        {
            AddIncludes(x => x.PrintedPublicationReport.Select(y => y.User), x => x.PrintedPublicationReport.Select(y => y.ThemeOfScientificWork));
            ApplyOrderByDescending(x => x.Date);
        }
    }
}