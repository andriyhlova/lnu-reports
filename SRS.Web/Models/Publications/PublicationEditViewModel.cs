using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Publications
{
    public class PublicationEditViewModel
    {
        public int Id { get; set; }

        [RequiredField]
        public Language Language { get; set; }

        public string OtherLanguage { get; set; }

        [RequiredField]
        public PublicationType PublicationType { get; set; }

        [RequiredField]
        public string Name { get; set; }

        public string MainAuthor { get; set; }

        [RequiredField]
        public string AuthorsOrder { get; set; }

        public string Place { get; set; }

        public string Edition { get; set; }

        public int Year { get; set; }

        public int? JournalId { get; set; }

        public string OtherJournal { get; set; }

        public string Link { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        public string PageFrom { get; set; }

        public string PageTo { get; set; }

        public string PublicationIdentifier { get; set; }

        public IList<UserInitialsModel> Users { get; set; }

        public int? NumberOfPages { get; set; }

        public string ISBN { get; set; }

        public string ConferenceName { get; set; }

        public string ConferenceDate { get; set; }

        public string ConferencePlace { get; set; }

        public string ConferenceCountry { get; set; }

        public string ConferenceEdition { get; set; }

        public string Issue { get; set; }

        public string ApplicationNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ApplicationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? PublicationDate { get; set; }

        public string ApplicationOwner { get; set; }

        public string BulletinNumber { get; set; }

        public string ChapterMonographyName { get; set; }

        public string Editors { get; set; }
    }
}
