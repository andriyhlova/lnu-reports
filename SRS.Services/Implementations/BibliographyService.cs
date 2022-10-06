using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Utilities;

namespace SRS.Services.Implementations
{
    public class BibliographyService : IBibliographyService
    {
        public string Get(Publication publication)
        {
            switch (publication.PublicationType)
            {
                case PublicationType.Монографія_У_Закордонному_Видавництві:
                case PublicationType.Монографія_У_Вітчизняному_Видавництві:
                case PublicationType.Підручник:
                case PublicationType.Навчальний_Посібник:
                case PublicationType.Інше_Наукове_Видання:
                case PublicationType.Розділ_монографії:
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
                $"{GetBibliographyPart(" / ", GetPartWithDot(publication.AuthorsOrder))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.Place, publication.Edition), publication.Date.Year.ToString())))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(publication.Tome))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(GetPagesPart(publication)))}" +
                $"{GetBibliographyPart(" - ISBN ", GetPartWithDot(publication.ISBN))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(publication.Link))}")
                .Trim();
        }

        private string GetConferenceBibliography(Publication publication)
        {
            return GetPartWithDot($"{publication.MainAuthor}" +
                $"{GetBibliographyPart(" ", publication.Name)}" +
                $"{GetBibliographyPart(" / ", publication.AuthorsOrder)}" +
                $"{GetBibliographyPart(" // ", StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.ConferenceName, publication.ConferenceEdition), publication.ConferencePlace, publication.ConferenceCountry, publication.ConferenceDate))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.Place, publication.Edition), publication.Date.Year.ToString())))}" +
                $"{GetBibliographyPart(" - ", GetPagesPart(publication))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(publication.Link))}")
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
                $"{GetBibliographyPart(" - ", GetPartWithDot(publication.Date.Year.ToString()))}" +
                $"{GetBibliographyPart(" - ", StringUtilities.JoinNotNullOrWhitespace(", ", publication.Tome, publication.Issue))}" +
                $"{GetBibliographyPart(" - ", GetPagesPart(publication))}" +
                $"{GetBibliographyPart(" - DOI: ", GetPartWithDot(publication.DOI))}" +
                $"{GetBibliographyPart(" - ", GetPartWithDot(publication.Link))}")
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
            return !string.IsNullOrWhiteSpace(pages) ? $"{pageTitle.ToUpper()} {GetPartWithDot(pages)}" : publication.PublicationIdentifier.ToString();
        }

        private string GetBibliographyPart(string prefix, string info)
        {
            return string.IsNullOrWhiteSpace(info) ? string.Empty : prefix + info;
        }

        private string GetPartWithDot(string part)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                return part;
            }

            return part + (!part.EndsWith(".") ? "." : string.Empty);
        }
    }
}
