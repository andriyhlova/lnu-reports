﻿using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models;
using System;
using System.Collections.Generic;

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

        public string Magazine { get; set; }

        public string Link { get; set; }

        public string DOI { get; set; }

        public string Tome { get; set; }

        public int? PageFrom { get; set; }

        public int? PageTo { get; set; }

        public IList<UserInitialsModel> Users { get; set; }

        public string GetPagesRange()
        {
            if (PageFrom.HasValue && PageTo.HasValue)
            {
                return PageFrom == PageTo ? PageFrom.ToString() : $"{PageFrom}-{PageTo}";
            }

            return null;
        }

        public double GetSizeOfPages()
        {
            if (PageFrom.HasValue && PageTo.HasValue)
            {
                return Math.Round((PageTo.Value - PageFrom.Value + 1) / 16.0, 1);
            }

            return 0.0;
        }
    }
}
