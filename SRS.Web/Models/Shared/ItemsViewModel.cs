using PagedList;

namespace SRS.Web.Models.Shared
{
    public class ItemsViewModel<TFilterModel, TItemsModel>
        where TFilterModel : BaseFilterViewModel
    {
        public TFilterModel FilterModel { get; set; }

        public StaticPagedList<TItemsModel> Items { get; set; }
    }
}
