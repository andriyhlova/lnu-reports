using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using AutoMapper;
using PagedList;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;

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