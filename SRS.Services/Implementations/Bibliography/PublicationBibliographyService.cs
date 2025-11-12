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
                case PublicationType.Монографія_У_Наукометричних_Базах:
                case PublicationType.Монографія_Опубліковані_За_Кордоном:
                case PublicationType.Монографія_Опубліковані_В_Україні:
                case PublicationType.Інші_монографії:
                case PublicationType.Підручник:
                case PublicationType.Навчальний_Посібник:
                case PublicationType.Інше_Наукове_Видання:
                case PublicationType.Розділ_монографії_У_Закордонному_Видавництві:
                case PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві:
                case PublicationType.Розділ_монографії_У_Наукометричних_Базах:
                case PublicationType.Розділ_монографії_Опубліковані_За_Кордоном:
                case PublicationType.Розділ_монографії_Опубліковані_В_Україні:
                    return GetBookBibliography(publication);
                case PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор:
                case PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних:
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q1_Q2:
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q3_Q4:
                case PublicationType.Стаття_В_Інших_Закордонних_Виданнях:
                case PublicationType.Стаття_В_Фахових_Виданнях_України:
                case PublicationType.Стаття_В_Інших_Виданнях_України:
                    return GetArticleBibliography(publication);
                case PublicationType.Тези_Доповіді_На_Міжнародній_Конференції:
                case PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції:
                case PublicationType.Матеріали_Конференцій_які_індексуються_у_міжнародних_наукометричних_базах_даних:
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
            var showTomePages = publication.PublicationType == PublicationType.Інше_Наукове_Видання && !string.IsNullOrEmpty(publication.GetPages());
            return GetPartWithDot($"{publication.MainAuthor}" +
                $"{GetBibliographyPart(" ", publication.Name)}" +
                $"{GetBibliographyPart(" / ", publication.AuthorsOrder)}" +
                $"{GetBibliographyPart(" // ", publication.ChapterMonographyName)}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", StringUtilities.JoinNotNullOrWhitespace(" : ", publication.Place, publication.Edition), publication.Date.Year.ToString())))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", publication.Tome, showTomePages ? GetNumberOfPages(publication) : null)))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(GetPagesPart(publication)))}" +
                $"{GetBibliographyPart($" {_dash} ISBN ", GetPartWithDot(publication.ISBN))}" +
                $"{GetBibliographyPart($" {_dash} ", GetReferencePart(publication))}")
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
                $"{GetBibliographyPart($" {_dash} ", GetPartWithDot(StringUtilities.JoinNotNullOrWhitespace(", ", publication.Tome, publication.Issue)))}" +
                $"{GetBibliographyPart($" {_dash} ", GetPagesPart(publication))}" +
                $"{GetBibliographyPart($" {_dash} ", GetReferencePart(publication))}")
                .Trim();
        }

        private string GetPagesPart(Publication publication)
        {
            var pageTitle = GetPageTitle(publication.Language);
            if (publication.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві ||
                publication.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві ||
                publication.PublicationType == PublicationType.Монографія_У_Наукометричних_Базах ||
                publication.PublicationType == PublicationType.Монографія_Опубліковані_За_Кордоном ||
                publication.PublicationType == PublicationType.Монографія_Опубліковані_В_Україні ||
                publication.PublicationType == PublicationType.Інші_монографії ||
                publication.PublicationType == PublicationType.Підручник ||
                publication.PublicationType == PublicationType.Навчальний_Посібник)
            {
                var numberOfPages = GetNumberOfPages(publication);
                return !string.IsNullOrWhiteSpace(numberOfPages) ? numberOfPages : GetPages(publication, pageTitle, false);
            }

            var pages = GetPages(publication, pageTitle.ToUpper(), true);
            if (publication.PublicationType == PublicationType.Інше_Наукове_Видання)
            {
                return !string.IsNullOrWhiteSpace(pages) ? pages : GetNumberOfPages(publication);
            }

            var identifierPagesPart = !string.IsNullOrWhiteSpace(pages) ? $" ({publication.GetPages()})" : string.Empty;
            return !string.IsNullOrWhiteSpace(publication.PublicationIdentifier)
                ? publication.PublicationIdentifier + identifierPagesPart
                : GetPartWithDot(pages);
        }

        private string GetPageTitle(Language language)
        {
            return language == Language.UA ? "с." : "p.";
        }

        private string GetNumberOfPages(Publication publication)
        {
            var pageTitle = GetPageTitle(publication.Language);
            return publication.NumberOfPages.HasValue ? $"{publication.NumberOfPages} {pageTitle}" : string.Empty;
        }

        private string GetPages(Publication publication, string title, bool atStart)
        {
            var pages = publication.GetPages();
            if (string.IsNullOrEmpty(pages))
            {
                return string.Empty;
            }

            return atStart ? $"{title} {pages}" : $"{pages} {title}";
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
