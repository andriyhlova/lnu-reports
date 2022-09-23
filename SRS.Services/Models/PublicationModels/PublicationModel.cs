using System;
using System.Collections.Generic;
using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Models.PublicationModels
{
    public class PublicationModel : BasePublicationModel
    {
        [RequiredField]
        public Language Language { get; set; }

        [RequiredField]
        public string MainAuthor { get; set; }

        public EditionCategory? EditionCategory { get; set; }

        public int? PageFrom { get; set; }

        public int? PageTo { get; set; }

        public string Link { get; set; }

        public string Edition { get; set; }

        public string Place { get; set; }

        public int? JournalId { get; set; }

        public string OtherJournal { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

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

        public DateTime? ApplicationDate { get; set; }

        public string ApplicationOwner { get; set; }

        public string BulletinNumber { get; set; }

        public double GetSizeOfPages()
        {
            var difference = NumberOfPages ?? (PageTo.GetValueOrDefault() - PageFrom.GetValueOrDefault() + 1);
            return Math.Round(difference / PublicationValues.FontSize, 1);
        }
    }
}
