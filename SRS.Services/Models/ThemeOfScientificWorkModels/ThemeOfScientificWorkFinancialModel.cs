using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkFinancialModel : BaseModel
    {
        public int Year { get; set; }

        public decimal Amount { get; set; }

        public int ThemeOfScientificWorkId { get; set; }
    }
}
