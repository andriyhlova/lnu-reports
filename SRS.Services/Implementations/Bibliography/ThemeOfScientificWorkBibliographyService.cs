using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Extensions;
using SRS.Services.Interfaces.Bibliography;
using SRS.Services.Models.Constants;

namespace SRS.Services.Implementations.Bibliography
{
    public class ThemeOfScientificWorkBibliographyService : BaseBibliographyService, IBibliographyService<ThemeOfScientificWork>
    {
        public string Get(ThemeOfScientificWork theme)
        {
            return GetPartWithDot($"{GetBibliographyPart(" ", theme.Code)}" +
                $"{GetBibliographyPart(" ", $"“{theme.Value}”")}" +
                $"{GetBibliographyPart(" - ", GetFinancialPart(theme))}" +
                $"{GetBibliographyPart("; ", GetThemeBlock("номер державної реєстрації", theme.ThemeNumber))}" +
                $"{GetBibliographyPart("; ", GetThemeBlock("термін виконання", GetDatePart(theme)))}" +
                $"{GetBibliographyPart("; ", GetThemeBlock("науковий керівник", theme.GetSupervisorWithTitles()))}")
                .Trim();
        }

        private string GetFinancialPart(ThemeOfScientificWork theme)
        {
            var financialText = theme.Financial != Financial.Other || string.IsNullOrWhiteSpace(theme.OtherProjectType) ? theme.Financial.GetDisplayName() : theme.OtherProjectType;
            return financialText?.TransformFirstLetter(char.ToLower) ?? string.Empty;
        }

        private string GetDatePart(ThemeOfScientificWork theme)
        {
            return $"{theme.PeriodFrom.ToString(Dates.UaDatePattern)}-{theme.PeriodTo.ToString(Dates.UaDatePattern)}";
        }

        private string GetThemeBlock(string title, string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? $"{title}: {value}" : string.Empty;
        }
    }
}
