using System;
using System.Collections.Generic;
using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models
{
    public class PublicationModel : BasePublicationModel
    {
        [RequiredField]
        public Language Language { get; set; }

        [RequiredField]
        public string MainAuthor { get; set; }

        public string Pages { get; set; }

        public double SizeOfPages { get; set; }

        public string Link { get; set; }

        public string Edition { get; set; }

        public string Place { get; set; }

        public string Magazine { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        public IList<UserInitialsModel> Users { get; set; }

        public int? GetPageNumber(int rangePosition)
        {
            try
            {
                var pages = Pages?.Trim('-').Replace("--", "-");
                if (string.IsNullOrEmpty(pages))
                {
                    return null;
                }

                if (pages.Contains("-"))
                {
                    return Convert.ToInt32(pages.Split('-')[rangePosition]);
                }

                return Convert.ToInt32(pages);
            }
            catch
            {
                return null;
            }
        }
    }
}
