using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class ThemeOfScientificWorkFinancialModel : BaseModel
    {
        public int Year { get; set; }

        public double Amount { get; set; }

        public int ThemeOfScientificWorkId { get; set; }
    }
}
