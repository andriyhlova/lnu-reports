using System.Collections.Generic;

namespace SRS.Services.Models.ReportModels
{
    public class ReportTemplateModel
    {
        public int Year { get; set; }

        public string Position { get; set; }

        public string Cathedra { get; set; }

        public string UserFullName { get; set; }

        public string DateOfBirth { get; set; }

        public int GraduationYear { get; set; }

        public int? AspStart { get; set; }

        public int? AspFinish { get; set; }

        public int? DocStart { get; set; }

        public int? DocFinish { get; set; }

        public int? DefenceYear { get; set; }

        public int? AcademicStatusYear { get; set; }

        public string ThemeOfScientificWorkFinancial { get; set; }

        public string ThemeOfScientificWorkTitle { get; set; }

        public string ThemeOfScientificWorkNumber { get; set; }

        public string ThemeOfScientificWorkHead { get; set; }

        public string ThemeOfScientificWorkPeriodFrom { get; set; }

        public string ThemeOfScientificWorkPeriodTo { get; set; }

        public string ThemeOfScientificWorkDescription { get; set; }

        public string ParticipationInGrands { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public string ApplicationForInevention { get; set; }

        public string PatentForInevention { get; set; }

        public string ReviewForTheses { get; set; }

        public string MembershipInCouncils { get; set; }

        public string Other { get; set; }

        public string Protocol { get; set; }

        public string Date { get; set; }

        public string CathedraLeadStatus { get; set; }

        public string CathedraLead { get; set; }

        public int MonographAllCount { get; set; }

        public int MonographPeriodCount { get; set; }

        public int BookAllCount { get; set; }

        public int BookPeriodCount { get; set; }

        public int TrainingBookAllCount { get; set; }

        public int TrainingBookPeriodCount { get; set; }

        public int ArticlesAllCount { get; set; }

        public int ArticlesPeriodCount { get; set; }

        public int OtherWritingsAllCount { get; set; }

        public int OtherWritingsPeriodCount { get; set; }

        public int ConferencesAllCount { get; set; }

        public int ConferencesPeriodCount { get; set; }

        public int PatentsAllCount { get; set; }

        public int PatentsPeriodCount { get; set; }

        public int PrintedPublicationCount =>
            Monographies.Count
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

        public List<string> Monographies { get; set; }

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
