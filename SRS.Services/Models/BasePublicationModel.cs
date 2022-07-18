using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models
{
    public class BasePublicationModel : BaseModel
    {
        [RequiredField]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime Date { get; set; }

        public PublicationType PublicationType { get; set; }

        public string AuthorsOrder { get; set; }

        public IList<string> UserIds { get; set; }
    }
}
