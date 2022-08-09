using System.Collections.Generic;
using System.Linq;

namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportPublicationsModel
    {
        public int AllPublicationsCount => Monographs.Count
            + Books.Count
            + TrainingBooks.Count
            + OtherWritings.Count
            + AllArticlesCount
            + AllConferencesCount;

        public List<CathedraReportPublicationModel> Monographs { get; set; }

        public double MonographsSize => Monographs.Sum(x => x.Size);

        public List<CathedraReportPublicationModel> Books { get; set; }

        public double BooksSize => Books.Sum(x => x.Size);

        public List<CathedraReportPublicationModel> TrainingBooks { get; set; }

        public double TrainingBooksSize => TrainingBooks.Sum(x => x.Size);

        public List<CathedraReportPublicationModel> OtherWritings { get; set; }

        public double OtherWritingsSize => OtherWritings.Sum(x => x.Size);

        public int AllArticlesCount => ImpactFactorArticles.Count
            + InternationalMetricArticles.Count
            + InternationalArticles.Count
            + OtherInternationalArticles.Count
            + NationalProfessionalArticles.Count
            + OtherNationalArticles.Count;

        public List<string> ImpactFactorArticles { get; set; }

        public List<string> InternationalMetricArticles { get; set; }

        public List<string> InternationalArticles { get; set; }

        public List<string> OtherInternationalArticles { get; set; }

        public List<string> NationalProfessionalArticles { get; set; }

        public List<string> OtherNationalArticles { get; set; }

        public int AllConferencesCount => InternationalConferences.Count + NationalConferences.Count;

        public List<string> InternationalConferences { get; set; }

        public List<string> NationalConferences { get; set; }
    }
}
