namespace SRS.Web.Models.Shared
{
    public class PageInfoViewModel
    {
        public int PageNumber { get; set; }

        public int? PageCount { get; set; }

        public int PageItemCount { get; set; }

        public int TotalItemCount { get; set; }
    }
}
