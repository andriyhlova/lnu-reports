using System;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Shared
{
    public class DateRangeViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? To { get; set; }
    }
}
