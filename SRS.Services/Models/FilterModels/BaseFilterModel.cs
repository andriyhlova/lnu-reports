namespace SRS.Services.Models.FilterModels
{
    public class BaseFilterModel
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public int? OrderBy { get; set; }

        public bool Desc { get; set; }

        public string Search { get; set; }
    }
}
