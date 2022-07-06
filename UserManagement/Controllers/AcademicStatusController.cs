using SRS.Services.Interfaces;
using SRS.Services.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class AcademicStatusController : Controller
    {
        private readonly IBaseCrudService<AcademicStatusModel> _academicStatusService;

        public AcademicStatusController(IBaseCrudService<AcademicStatusModel> academicStatusService)
        {
            _academicStatusService = academicStatusService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _academicStatusService.GetAllAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcademicStatusModel academicStatus)
        {
            if (ModelState.IsValid)
            {
                await _academicStatusService.AddAsync(academicStatus);
                return RedirectToAction(nameof(Index));
            }

            return View(academicStatus);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var academicStatus = await _academicStatusService.GetAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }

            return View(academicStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AcademicStatusModel academicStatus)
        {
            if (ModelState.IsValid)
            {
                await _academicStatusService.UpdateAsync(academicStatus);
                return RedirectToAction("Index");
            }

            return View(academicStatus);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var academicStatus = await _academicStatusService.GetAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }

            return View(academicStatus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _academicStatusService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
