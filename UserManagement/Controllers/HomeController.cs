using System.Web.Mvc;

namespace SRS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
               return View();
        }
    }
}