using AutoMapper;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IBaseCrudService<FacultyModel> _facultiesCrudService;
        private readonly IFacultyService _facultiesService;
        private readonly IMapper _mapper;

        public AnalyticsController(
            IBaseCrudService<FacultyModel> facultiesCrudService,
            IFacultyService facultiesService,
            IMapper mapper)
        {
            _facultiesCrudService = facultiesCrudService;
            _facultiesService = facultiesService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}