﻿using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.UserModels
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Old name")]
    public class I18nUserInitialsModel : BaseModel
    {
        public Language Language { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FathersName { get; set; } = string.Empty;

        public string UserId { get; set; }
    }
}