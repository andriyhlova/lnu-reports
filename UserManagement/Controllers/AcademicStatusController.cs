using AutoMapper;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class AcademicStatusController : BaseController
    {
        private readonly IServiceBase<AcademicStatus,int> academicStatusService;

        public AcademicStatusController(IServiceBase<AcademicStatus,int> academicStatusService,
            IMapper mapper): base(mapper)
        {
            this.academicStatusService = academicStatusService;
        }
        public async Task<ActionResult> Index()
        {
            var academicStatuses = await academicStatusService.GetAllAsync();
            var data = mapper.Map<IEnumerable<AcademicStatus>, IEnumerable<AcademicStatusViewModel>>(academicStatuses);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcademicStatusViewModel academicStatusModel)
        {
            if (ModelState.IsValid)
            {
                var academicStatus = mapper.Map<AcademicStatusViewModel, AcademicStatus>(academicStatusModel);
                await academicStatusService.CreateAsync(academicStatus);
                return RedirectToAction("Index");
            }

            return View(academicStatusModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var academicStatus = await academicStatusService.GetByIdAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }
            var academicStatusModel = mapper.Map<AcademicStatus, AcademicStatusViewModel>(academicStatus);
            return View(academicStatusModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AcademicStatusViewModel academicStatusModel)
        {
            if (ModelState.IsValid)
            {
                var academicStatus = mapper.Map<AcademicStatusViewModel, AcademicStatus>(academicStatusModel);
                await academicStatusService.UpdateAsync(academicStatus);
                return RedirectToAction("Index");
            }
            return View(academicStatusModel);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var academicStatus = await academicStatusService.GetByIdAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }
            var academicStatusModel = mapper.Map<AcademicStatus, AcademicStatusViewModel>(academicStatus);
            return View(academicStatusModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await academicStatusService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
