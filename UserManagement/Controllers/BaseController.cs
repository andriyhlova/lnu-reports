using AutoMapper;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper mapper;
        public BaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}