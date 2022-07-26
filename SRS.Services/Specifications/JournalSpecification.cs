using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class JournalSpecification : BaseFilterSpecification<Journal>
    {
        public JournalSpecification(BaseFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => string.IsNullOrEmpty(filterModel.Search)
                        || x.Name.Contains(filterModel.Search)
                        || x.ShortName.Contains(filterModel.Search)
                        || x.PrintIssn.Contains(filterModel.Search)
                        || x.ElectronicIssn.Contains(filterModel.Search),
                  true)
        {
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
                default: ApplyOrderBy(x => x.Name); break;
            }
        }
    }
}
