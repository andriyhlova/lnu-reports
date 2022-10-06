using System;
using SRS.Domain.Enums;

namespace SRS.Services.Models.FilterModels
{
    public class PublicationFilterModel : DepartmentFilterModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }

        public PublicationType? PublicationType { get; set; }
    }
}
