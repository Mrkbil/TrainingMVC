using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Training.DataAccess.Repositories;
using Training.Model.DTO;
using TrainingMVC.Model.Entity;

namespace TrainingMVC.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Designation
        public async Task<IActionResult> Index()
        {
            var designations = await _unitOfWork.DesignationRepo.GetAll();
            return View(designations);
        }

        // GET: Designation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Designation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DesignationDto designationDto)
        {
            if (ModelState.IsValid)
            {
                var designation = new Designation
                {
                    Name = designationDto.Name
                };
                _unitOfWork.DesignationRepo.Add(designation);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designationDto);
        }

        // GET: Designation/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var designation = await _unitOfWork.DesignationRepo.GetById(id);
            if (designation == null)
            {
                return NotFound();
            }

            var designationDto = new DesignationDto
            {
                Id = designation.Id.ToString(),
                Name = designation.Name
            };
            return View(designationDto);
        }

        // POST: Designation/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DesignationDto designationDto)
        {
            if (id != designationDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var designation = new Designation
                {
                    Id = designationDto.Id,
                    Name = designationDto.Name
                };

                _unitOfWork.DesignationRepo.Edit(designation);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designationDto);
        }

        // GET: Designation/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var designation = await _unitOfWork.DesignationRepo.GetById(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        // POST: Designation/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var designation = await _unitOfWork.DesignationRepo.GetById(id);
            if (designation != null)
            {
                _unitOfWork.DesignationRepo.Delete(designation);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
