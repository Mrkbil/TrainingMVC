using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Training.DataAccess.Repositories;
using TrainingMVC.Model.Entity;
using TrainingMVC.Model;

namespace TrainingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepo.GetAll();
            var employees = await _unitOfWork.EmployeeRepo.GetAll();
            var designations = await _unitOfWork.DesignationRepo.GetAll();

            // Create a ViewModel to pass all data to the view
            var viewModel = new HomeViewModel
            {
                Departments = departments,
                Employees = employees,
                Designations = designations
            };

            return View(viewModel);
        }
    }
}
