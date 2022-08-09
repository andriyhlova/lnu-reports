using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.IndividualReport
{
    public class ReportPublicationsModel
    {
        public int PrintedPublicationCount =>
            Monographs.Count
            + Books.Count
            + TrainingBooks.Count
            + OtherWritings.Count
            + ArticlesCount
            + ConferencesCount
            + RecommendedPublications.Count
            + RecommendedMonographs.Count
            + RecommendedBooks.Count
            + RecommendedTrainingBooks.Count
            + RecommendedOtherWritings.Count
            + AcceptedToPrintPublications.Count;

        public List<string> Monographs { get; set; }

        public List<string> Books { get; set; }

        public List<string> TrainingBooks { get; set; }

        public List<string> OtherWritings { get; set; }

        public int ArticlesCount =>
            ImpactFactorArticles.Count
            + InternationalMetricArticles.Count
            + OtherInternationalArticles.Count
            + NationalProfessionalArticles.Count
            + OtherNationalArticles.Count;

        public List<string> ImpactFactorArticles { get; set; }

        public List<string> InternationalMetricArticles { get; set; }

        public List<string> OtherInternationalArticles { get; set; }

        public List<string> NationalProfessionalArticles { get; set; }

        public List<string> OtherNationalArticles { get; set; }

        public int ConferencesCount =>
            InternationalConferences.Count
            + NationalConferences.Count;

        public List<string> InternationalConferences { get; set; }

        public List<string> NationalConferences { get; set; }

        public List<string> RecommendedPublications { get; set; }

        public List<string> RecommendedMonographs { get; set; }

        public List<string> RecommendedBooks { get; set; }

        public List<string> RecommendedTrainingBooks { get; set; }

        public List<string> RecommendedOtherWritings { get; set; }

        public List<string> AcceptedToPrintPublications { get; set; }
    }
}
