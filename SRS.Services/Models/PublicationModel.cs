using System;
using System.Collections.Generic;
using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.Constants;

namespace SRS.Services.Models
{
    public class PublicationModel : BasePublicationModel
    {
        [RequiredField]
        public Language Language { get; set; }

        [RequiredField]
        public string MainAuthor { get; set; }

        public int? PageFrom { get; set; }

        public int? PageTo { get; set; }

        public string Link { get; set; }

        public string Edition { get; set; }

        public string Place { get; set; }

        public string Magazine { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        public IList<UserInitialsModel> Users { get; set; }

        public double GetSizeOfPages()
        {
            if (PageFrom.HasValue && PageTo.HasValue)
            {
                return Math.Round((PageTo.Value - PageFrom.Value + 1) / PublicationValues.FontSize, 1);
            }

            return 0.0;
        }
    }
}
