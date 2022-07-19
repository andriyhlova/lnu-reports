using SRS.Domain.Enums;

namespace SRS.Services.Models
{
    public class PublicationModel : BasePublicationModel
    {
        public double SizeOfPages { get; set; }

        public Language Language { get; set; }

        public string Link { get; set; }

        public string Edition { get; set; }

        public string Place { get; set; }

        public string Magazine { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }
    }
}
