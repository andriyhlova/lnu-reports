using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.Constants;
using SRS.Services.Models.UserModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Publications
{
    public class PublicationEditViewModel
    {
        public int Id { get; set; }

        [RequiredField]
        public Language Language { get; set; }

        [RequiredField]
        public PublicationType PublicationType { get; set; }

        [RequiredField]
        public string Name { get; set; }

        [RequiredField]
        public string MainAuthor { get; set; }

        [RequiredField]
        public string AuthorsOrder { get; set; }

        public string Place { get; set; }

        [RequiredField]
        public int Year { get; set; }

        public string Edition { get; set; }

        public int? JournalId { get; set; }

        public string OtherJournal { get; set; }

        public string Link { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        [Range(PublicationValues.MinPageNumber, int.MaxValue, ErrorMessage = "Неправильний номер сторінки")]
        public int? PageFrom { get; set; }

        [Range(PublicationValues.MinPageNumber, int.MaxValue, ErrorMessage = "Неправильний номер сторінки")]
        public int? PageTo { get; set; }

        public IList<UserInitialsModel> Users { get; set; }
    }
}
