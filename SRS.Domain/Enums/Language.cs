﻿using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Language
    {
        [Display(Name = "Українська")]
        UA,
        [Display(Name = "Англійська")]
        EN,
        [Display(Name = "Німецька")]
        DE,
        [Display(Name = "Французька")]
        FR,
        [Display(Name = "Польська")]
        PL,
        [Display(Name = "Італіська")]
        IT,
        [Display(Name = "Російська")]
        RU,
        [Display(Name = "Інша")]
        Other
    }
}
