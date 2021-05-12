using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IPublicationService
    {
        String GenerateNameOfPublication(Publication publication);
        string GenerateNameOfPublications(List<Publication> publications);
        string GetPunktSixOne(Report report);
        string GetPunktSixOneMono(Report report);
        string GetPunktSixOneBook(Report report);
        string GetPunktSixOneTrainingBook(Report report);
        string GetPunktSixOneArticlesFactor(Report report);
        string GetPunktSixOneOther(Report report);
        string GetPunktSixOneArticlesOtherInterantional(Report report);
        string GetPunktSixOneArticlesInterantionalMetricals(Report report);
        string GetPunktSixOneArticlesNationalFah(Report report);
        string GetPunktSixOneArticlesNational(Report report);
        string GetPunktSixOneConferences(Report report);
        string GetPunktSixOneInternationalConferences(Report report);
        string GetPunktSixOneNationalConferences(Report report);
        string GetPunktSixTwo(Report report);
        string GetPunktSixTwoMono(Report report);
        string GetPunktSixTwoBook(Report report);
        string GetPunktSixTwoTrainingBook(Report report);
        string GetPunktSixTwoOther(Report report);
        string GetPunktSixTwoFive(Report report);
        string GetPunktSixOneArticles(Report report);
    }
}
