using SRS.Domain.Entities;
using SRS.Domain.Enums;

namespace SRS.Services.Implementations
{
    public class BibliographyService : IBibliographyService
    {
        public string Get(Publication publication)
        {
            return $"{publication.MainAuthor} {publication.Name} /  {publication.AuthorsOrder} // {GetJournalPart(publication)}{GetEditionPart(publication)}{GetPlacePart(publication)}– {publication.Date.Year}.{GetEndOfPublication(publication)}";
        }

        private string GetJournalPart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.GetJournalName()) ? publication.GetJournalName() + ". " : string.Empty;
        }

        private string GetEditionPart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.Edition) ? publication.Edition + ". " : string.Empty;
        }

        private string GetPlacePart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.Place) ? publication.Place + ". " : string.Empty;
        }

        private string GetEndOfPublication(Publication publication)
        {
            var toReturn = $" – {GetTomePart(publication)}";
            if (publication.PublicationType == PublicationType.Монографія
                        || publication.PublicationType == PublicationType.Підручник
                        || publication.PublicationType == PublicationType.Навчальний_Посібник)
            {
                toReturn += $"{publication.GetPages()}  {GetPageTitle(publication)}. ";
            }
            else
            {
                toReturn += $"{(!FinishesWithHyphen(toReturn) ? "–" : string.Empty)} {GetPageTitle(publication).ToUpper()}. {GetPagePart(publication)}";
            }

            toReturn += GetDoiPart(publication);
            return toReturn;
        }

        private bool FinishesWithHyphen(string text)
        {
            return text.EndsWith("-") || text.EndsWith("–") || text.EndsWith("- ") || text.EndsWith("– ");
        }

        private string GetTomePart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.Tome) ? publication.Tome + ". " : string.Empty;
        }

        private string GetPageTitle(Publication publication)
        {
            return publication.Language == Language.EN ? "p" : "с";
        }

        private string GetPagePart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.GetPages()) ? (publication.GetPages() + ". ") : string.Empty;
        }

        private string GetDoiPart(Publication publication)
        {
            return !string.IsNullOrEmpty(publication.DOI) ? "(" + publication.DOI + ")." : string.Empty;
        }
    }
}
