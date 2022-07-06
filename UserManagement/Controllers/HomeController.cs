using System.Web.Mvc;

namespace UserManagement.Controllers
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