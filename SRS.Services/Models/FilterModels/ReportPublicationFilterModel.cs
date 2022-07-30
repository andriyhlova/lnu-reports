using System;

namespace SRS.Services.Models.FilterModels
{
    public class ReportPublicationFilterModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }
    }
}
