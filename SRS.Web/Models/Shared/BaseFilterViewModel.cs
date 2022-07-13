namespace SRS.Web.Models.Shared
{
    public class BaseFilterViewModel
    {
        public int? Page { get; set; } = 1;

        public int? Take { get; set; }

        public int? OrderBy { get; set; }

        public bool Desc { get; set; }

        public string Search { get; set; }
    }
}
