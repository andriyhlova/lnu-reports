using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Interfaces.Bibliography;
using SRS.Services.Utilities;

namespace SRS.Services.Implementations.Bibliography
{
    public class PublicationBibliographyService : BaseBibliographyService, IBibliographyService<Publication>
    {
        private const string _dash = "\u2013";

        public string Get(Publication publication)
        {
            switch (publication.PublicationType)
            {
                case PublicationType.Монографія_У_Закордонному_Видавництві:
                case PublicationType.Монографія_У_Вітчизняному_Видавництві:
                case PublicationType.Підручник:
                case PublicationType.Навчальний_Посібник:
                case PublicationType.Інше_Наукове_Видання:
                case PublicationType.Розділ_монографії_У_Закордонному_Видавництві:
                case PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві:
                    return GetBookBibliography(publication);
                case PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор:
                case PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних:
                case PublicationType.Стаття_В_Інших_Закордонних_Виданнях:
                case PublicationType.Стаття_В_Фахових_Виданнях_України:
                case PublicationType.Стаття_В_Інших_Виданнях_України:
                    return GetArticleBibliography(publication);
                case PublicationType.Тези_Доповіді_На_Міжнародній_Конференції:
                case PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції:
                    return GetConferenceBibliography(publication);
                case PublicationType.Заявка_на_винахід:
                case PublicationType.Патент:
                    return GetInventionBibliography(publication);
                default:
                    return GetArticleBibliography(publication);
            }
        }

        private string GetBookBibliography(Publication publication)
        {
            return GetPartWithDot($"{publication.MainAuthor}" +
                $"{GetBibliographyPart(" ", publication.Name)}" +
                $"{GetBibliographyPart(" / ", publication.AuthorsOrder)}" +
                $"{GetBibliographyPart(" // ", publication.ChapterMonographyName)}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.Place, publication.Edition), publication.Date.Year.ToString())))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(publication.Tome))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(GetPagesPart(publication)))}" +
                $"{GetBibliographyPart($" {_dash} ISBN ", GetPartWithDot(publication.ISBN))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(publication.Link))}")
                .Trim();
        }

        private string GetConferenceBibliography(Publication publication)
        {
            return GetPartWithDot($"{publication.MainAuthor}" +
                $"{GetBibliographyPart(" ", publication.Name)}" +
                $"{GetBibliographyPart(" / ", publication.AuthorsOrder)}" +
                $"{GetBibliographyPart(" // ", StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.ConferenceName, publication.ConferenceEdition), publication.ConferencePlace, publication.ConferenceCountry, publication.ConferenceDate))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.Place, publication.Edition), publication.Date.Year.ToString())))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPagesPart(publication))}")
                .Trim();
        }

        private string GetInventionBibliography(Publication publication)
        {
            return string.Join(
                ", ",
                publication.AuthorsOrder,
                publication.Name,
                publication.ApplicationNumber,
                publication.Date.ToString("dd.MM.yyyy"),
                publication.ApplicationOwner);
        }

        private string GetArticleBibliography(Publication publication)
        {
            return GetPartWithDot($"{publication.MainAuthor}" +
                $"{GetBibliographyPart(" ", publication.Name)}" +
                $"{GetBibliographyPart(" / ", publication.AuthorsOrder)}" +
                $"{GetBibliographyPart(" // ", GetPartWithDot(publication.GetJournalName(true)))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(publication.Date.Year.ToString()))}" +
                $"{GetBibliographyPart($" {_dash} ", StringUtilities.JoinNotNullOrWhitespace(", ", publication.Tome, publication.Issue))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPagesPart(publication))}" +
                $"{GetBibliographyPart($" {_dash} ", GetReferencePart(publication))}")
                .Trim();
        }

        private string GetPagesPart(Publication publication)
        {
            var pageTitle = publication.Language == Language.UA ? "с." : "p.";
            if (publication.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві ||
                publication.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві ||
                publication.PublicationType == PublicationType.Підручник ||
                publication.PublicationType == PublicationType.Навчальний_Посібник ||
                publication.PublicationType == PublicationType.Інше_Наукове_Видання)
            {
                return $"{publication.NumberOfPages?.ToString() ?? publication.GetPages()}  {pageTitle}";
            }

            var pages = publication.GetPages();
            var pagesPart = !string.IsNullOrWhiteSpace(pages) ? $"{pageTitle.ToUpper()} {pages}" : string.Empty;
            var identifierPagesPart = !string.IsNullOrWhiteSpace(pages) ? $" ({pages})" : string.Empty;
            return !string.IsNullOrWhiteSpace(publication.PublicationIdentifier)
                ? publication.PublicationIdentifier + identifierPagesPart
                : GetPartWithDot(pagesPart);
        }

        private string GetReferencePart(Publication publication)
        {
            if (!string.IsNullOrWhiteSpace(publication.DOI))
            {
                return "DOI: " + GetPartWithDot(publication.DOI);
            }

            return GetPartWithDot(publication.Link);
        }
    }
}
