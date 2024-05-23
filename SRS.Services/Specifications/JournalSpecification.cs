﻿using System.Linq;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class JournalSpecification : BaseFilterSpecification<Journal>
    {
        public JournalSpecification(JournalFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => (filterModel.PublicationType == null || x.JournalTypes.Any(y => y.PublicationType == filterModel.PublicationType)) &&
                          (string.IsNullOrEmpty(filterModel.Search)
                                || x.Name.Contains(filterModel.Search)
                                || x.ShortName.Contains(filterModel.Search)
                                || x.PrintIssn.Contains(filterModel.Search)
                                || x.ElectronicIssn.Contains(filterModel.Search)),
                  true)
        {
            AddInclude(journal => journal.JournalTypes);
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((JournalOrderType?)orderBy)
            {
                case JournalOrderType.Name when !desc: ApplyOrderBy(x => x.Name); break;
                case JournalOrderType.Name when desc: ApplyOrderByDescending(x => x.Name); break;
                case JournalOrderType.ShortName when !desc: ApplyOrderBy(x => x.ShortName); break;
                case JournalOrderType.ShortName when desc: ApplyOrderByDescending(x => x.ShortName); break;
                case JournalOrderType.PrintIssn when !desc: ApplyOrderBy(x => x.PrintIssn); break;
                case JournalOrderType.PrintIssn when desc: ApplyOrderByDescending(x => x.PrintIssn); break;
                case JournalOrderType.ElectronicIssn when !desc: ApplyOrderBy(x => x.ElectronicIssn); break;
                case JournalOrderType.ElectronicIssn when desc: ApplyOrderByDescending(x => x.ElectronicIssn); break;
                case JournalOrderType.Quartile when !desc: ApplyOrderBy(x => x.BestQuartile); break;
                case JournalOrderType.Quartile when desc: ApplyOrderByDescending(x => x.BestQuartile); break;
                case JournalOrderType.JournalType when !desc: ApplyOrderBy(x => x.JournalTypes.OrderBy(r => r.Value).Select(r => r.Value).FirstOrDefault()); break;
                case JournalOrderType.JournalType when desc: ApplyOrderByDescending(x => x.JournalTypes.OrderBy(r => r.Value).Select(r => r.Value).FirstOrDefault()); break;
                default: ApplyOrderBy(x => x.Name); break;
            }
        }
    }
}
