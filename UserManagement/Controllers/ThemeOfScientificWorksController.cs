using PagedList;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using ScientificReport.Services.Abstraction;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ThemeOfScientificWorksController : Controller
    {
        private readonly IThemeOfScientificWorksService _themeOfScientificWorksService;
        private readonly IUserService _userService;
        private readonly ICathedraService _cathedraService;

        public ThemeOfScientificWorksController(IThemeOfScientificWorksService themeOfScientificWorksService, IUserService userService, ICathedraService cathedraService)
        {
            _themeOfScientificWorksService = themeOfScientificWorksService;
            _userService = userService;
            _cathedraService = cathedraService;
        }

        // GET: ThemeOfScientificWorks
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var user = _userService.GetCurrentUser(User.Identity.Name);
            var scientifthemes = _themeOfScientificWorksService.GetScientificThemesByCathedraId(user.Cathedra.Id);
            return View(scientifthemes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ThemeOfScientificWorks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = await _themeOfScientificWorksService.GetByIdAsync(id.Value);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Create
        public ActionResult Create()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower().Replace('_', ' '), Value = x }).ToList();
            return View();
        }

        // POST: ThemeOfScientificWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,Financial,ThemeNumber,Code")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetCurrentUser(User.Identity.Name);
                themeOfScientificWork.Cathedra = await _cathedraService.GetByIdAsync(user.Cathedra.Id);
                await _themeOfScientificWorksService.CreateAsync(themeOfScientificWork);
                return RedirectToAction("Index");
            }

            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = await _themeOfScientificWorksService.GetByIdAsync(id.Value);
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower(), Value = x }).ToList();
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,ThemeNumber,Financial,Code")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                await _themeOfScientificWorksService.UpdateAsync(themeOfScientificWork);
                return RedirectToAction("Index");
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = await _themeOfScientificWorksService.GetByIdAsync(id.Value);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _themeOfScientificWorksService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
