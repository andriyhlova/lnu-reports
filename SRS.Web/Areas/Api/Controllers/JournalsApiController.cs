using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRS.Web.Areas.Api.Controllers
{
    [Authorize]
    public class JournalsApiController : Controller
    {
        private const int SearchResultsLimit = 50;

        private readonly IJournalService _journalService;

        public JournalsApiController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpGet]
        public async Task<ActionResult> GetByPublicationType(PublicationType? publicationType, string search)
        {
            var journals = await _journalService.GetAllAsync(new JournalFilterModel
            {
                PublicationType = publicationType,
                OrderBy = (int)JournalOrderType.Name,
                Search = search,
                Skip = 0,
                Take = SearchResultsLimit
            });
            var requiredQuartile = GetRequiredQuartile(publicationType);
            var result = journals.Select(x => new
            {
                x.Id,
                x.Name,
                Quartile = x.BestQuartile.GetDisplayName(),
                Matches = requiredQuartile.HasValue ? (bool?)(x.BestQuartile == requiredQuartile.Value) : null
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetById(int id, PublicationType? publicationType)
        {
            var journal = await _journalService.GetFirstOrDefaultAsync(id);
            if (journal == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var requiredQuartile = GetRequiredQuartile(publicationType);
            var result = new
            {
                journal.Id,
                journal.Name,
                Quartile = journal.BestQuartile.GetDisplayName(),
                Matches = requiredQuartile.HasValue ? (bool?)(journal.BestQuartile == requiredQuartile.Value) : null
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static Quartile? GetRequiredQuartile(PublicationType? publicationType)
        {
            switch (publicationType)
            {
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q1:
                    return Quartile.Q1;
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q2:
                    return Quartile.Q2;
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q3:
                    return Quartile.Q3;
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_Q4:
                    return Quartile.Q4;
                case PublicationType.Стаття_У_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних_без_квартиля:
                    return Quartile.None;
                default:
                    return null;
            }
        }
    }
}
