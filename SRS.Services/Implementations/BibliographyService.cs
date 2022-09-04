using SRS.Domain.Entities;
using SRS.Domain.Enums;

namespace SRS.Services.Implementations
{
    public class BibliographyService : IBibliographyService
    {
        public string Get(Publication publication)
        {
            return $"{publication.MainAuthor} " +
                $"{publication.Name} / " +
                $"{publication.AuthorsOrder} // " +
                $"{GetPublicationPart(publication.GetJournalName())} " +
                $"{GetPublicationPart(publication.Edition)} " +
                $"{GetPublicationPart(publication.Place)} " +
                $"– {GetPublicationPart(publication.Date.Year.ToString())} " +
                $"– {GetPublicationPart(publication.Tome)} " +
                $"{GetPublicationPagePart(publication)} " +
                $"{GetPublicationDoiPart(publication)} "
                .Trim();
        }

        private string GetPublicationPagePart(Publication publication)
        {
            var pageTitle = publication.Language != Language.UA ? "с" : "p";
            var toReturn = string.Empty;
            if (publication.PublicationType == PublicationType.Монографія
                        || publication.PublicationType == PublicationType.Підручник
                        || publication.PublicationType == PublicationType.Навчальний_Посібник
                        || publication.PublicationType == PublicationType.Інше_Наукове_Видання)
            {
                return $"{publication.NumberOfPages?.ToString() ?? publication.GetPages()}  {pageTitle}.";
            }

            var finishesWithHyphen = !toReturn.Trim().EndsWith("-");
            return $"{(!finishesWithHyphen ? "–" : string.Empty)} {pageTitle.ToUpper()}. {GetPublicationPart(publication.GetPages())}";
        }

        private string GetPublicationDoiPart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.DOI) ? GetPublicationPart("(" + publication.DOI + ")") : string.Empty;
        }

        private string GetPublicationPart(string part)
        {
            if (string.IsNullOrEmpty(part))
            {
                return part;
            }

            return part + (!part.EndsWith(".") ? "." : string.Empty);
        }
    }
}
