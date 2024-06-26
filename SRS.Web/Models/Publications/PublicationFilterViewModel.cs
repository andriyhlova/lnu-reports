﻿using System;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Publications
{
    public class PublicationFilterViewModel : DepartmentFilterViewModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }
    }
}
