using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class Publication : BaseEntity
    {
        public Publication()
        {
            this.User = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public string OtherAuthors { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime Date { get; set; }

        public int? PageFrom { get; set; }

        public int? PageTo { get; set; }

        public int? PublicationIdentifier { get; set; }

        public double SizeOfPages { get; set; }

        public PublicationType PublicationType { get; set; }

        public Language Language { get; set; }

        public string OtherLanguage { get; set; }

        public string Link { get; set; }

        public string Edition { get; set; }

        public int? JournalId { get; set; }

        public string OtherJournal { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        public string Place { get; set; }

        public string MainAuthor { get; set; }

        public string AuthorsOrder { get; set; }

        public bool IsMainAuthorRegistered { get; set; }

        public int? NumberOfPages { get; set; }

        public string ISBN { get; set; }

        public string ConferenceName { get; set; }

        public string ConferenceDate { get; set; }

        public string ConferencePlace { get; set; }

        public string ConferenceCountry { get; set; }

        public string ConferenceEdition { get; set; }

        public string Issue { get; set; }

        public string ApplicationNumber { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string ApplicationOwner { get; set; }

        public string BulletinNumber { get; set; }

        public virtual Journal Journal { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }

        public virtual ICollection<Report> StudentPublicationReport { get; set; }

        public virtual ICollection<Report> PrintedPublicationReport { get; set; }

        public virtual ICollection<Report> RecomendedPublicationReport { get; set; }

        public virtual ICollection<Report> AcceptedToPrintPublicationReport { get; set; }

        public virtual ICollection<Report> ApplicationsForInventionReport { get; set; }

        public virtual ICollection<Report> PatentsForInventionReport { get; set; }

        public virtual ICollection<CathedraReport> PrintedPublicationBudgetCathedraReport { get; set; }

        public virtual ICollection<CathedraReport> PrintedPublicationInWorkCathedraReport { get; set; }

        public virtual ICollection<CathedraReport> PrintedPublicationHospDohovirCathedraReport { get; set; }

        public string GetPages()
        {
            if (PageFrom.HasValue && PageTo.HasValue && PageFrom.Value == PageTo.Value)
            {
                return PageFrom.Value.ToString();
            }

            var pages = new int?[] { PageFrom, PageTo };
            return string.Join("-", pages.Where(x => x.HasValue && x != 0));
        }

        public string GetJournalName(bool useShortName = false)
        {
            return (useShortName && !string.IsNullOrWhiteSpace(Journal?.ShortName) ? Journal?.ShortName : Journal?.Name) ?? OtherJournal;
        }

        public bool IsArticle()
        {
            return PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України
                || PublicationType == PublicationType.Стаття_В_Інших_Закордонних_Виданнях
                || PublicationType == PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор
                || PublicationType == PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних
                || PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України;
        }

        public bool IsConference()
        {
            return PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції
                || PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції;
        }
    }
}