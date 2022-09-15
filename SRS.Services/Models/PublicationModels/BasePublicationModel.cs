using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.PublicationModels
{
    public class BasePublicationModel : BaseModel
    {
        [RequiredField]
        public string Name { get; set; }

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime Date { get; set; }

        [RequiredField]
        public PublicationType PublicationType { get; set; }

        [RequiredField]
        public string AuthorsOrder { get; set; }

        public string JournalName { get; set; }

        public IList<string> UserIds { get; set; }
    }
}
