using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications
{
    public class ThemeOfScientificWorkWithFinancialsSpecification : ThemeOfScientificWorkSpecification
    {
        public ThemeOfScientificWorkWithFinancialsSpecification(ThemeOfScientificWorkFilterModel filterModel, Expression<Func<ThemeOfScientificWork, bool>> expression)
            : base(
                filterModel,
                expression.AndAlso(x => (filterModel.FacultyId == null || x.ThemeOfScientificWorkCathedras.Any(y => y.Cathedra.FacultyId == filterModel.FacultyId)) &&
                  (filterModel.CathedraId == null || x.ThemeOfScientificWorkCathedras.Any(y => y.CathedraId == filterModel.CathedraId))))
        {
            AddInclude(x => x.ThemeOfScientificWorkFinancials);
            AddInclude(x => x.ThemeOfScientificWorkCathedras.Select(y => y.Cathedra.Faculty));
        }
    }
}
