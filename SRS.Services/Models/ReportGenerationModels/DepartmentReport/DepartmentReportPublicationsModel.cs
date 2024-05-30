using System.Collections.Generic;
using System.Linq;

namespace SRS.Services.Models.ReportGenerationModels.DepartmentReport
{
    public class DepartmentReportPublicationsModel
    {
        public int AllPublicationsCount => Monographs.Count
            + Books.Count
            + TrainingBooks.Count
            + OtherWritings.Count
            + AllArticlesCount
            + AllConferencesCount;

        public List<DepartmentReportPublicationModel> Monographs { get; set; }

        public double MonographsSize => Monographs.Sum(x => x.Size);

        public List<DepartmentReportPublicationModel> Books { get; set; }

        public double BooksSize => Books.Sum(x => x.Size);

        public List<DepartmentReportPublicationModel> TrainingBooks { get; set; }

        public double TrainingBooksSize => TrainingBooks.Sum(x => x.Size);

        public List<DepartmentReportPublicationModel> OtherWritings { get; set; }

        public double OtherWritingsSize => OtherWritings.Sum(x => x.Size);

        public int AllArticlesCount => ImpactFactorArticles.Count
            + InternationalMetricArticles.Count
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

        public List<string> ApplicationsForInvention { get; set; }

        public List<string> PatentsForInvention { get; set; }
    }
}
