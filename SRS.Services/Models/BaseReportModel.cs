﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
{
    public class BaseReportModel : BaseModel
    {
        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public bool IsSigned { get; set; }

        public bool IsConfirmed { get; set; }

        public string UserId { get; set; }

        public IList<I18nUserInitialsModel> I18nUserInitials { get; set; }
    }
}
