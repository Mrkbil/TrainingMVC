using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Training.DataAccess.Repositories;
using TrainingMVC.Model.DTO;
using TrainingMVC.Model.Entity;

namespace TrainingMVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepo.GetAll();
            return View(departments);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeptListDTO departmentDto)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = departmentDto.Name
                };
                _unitOfWork.DepartmentRepo.Add(department);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        // GET: Department/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            var department = await _unitOfWork.DepartmentRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepo.Edit(department);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        // GET: Department/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            var department = await _unitOfWork.DepartmentRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var department = await _unitOfWork.DepartmentRepo.GetById(id);
            if (department != null)
            {
                _unitOfWork.DepartmentRepo.Delete(department);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
