using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrainingMVC.Model.DTO;
using TrainingMVC.Model.Entity;
using Training.DataAccess.Repositories;

public class EmployeeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        var employees = await _unitOfWork.EmployeeRepo.GetAll();
        var employeeDtos = employees.Select(e => new EmpListDTO
        {
            Id = e.Id,
            Name = e.Name,
            Address = e.Address,
            City = e.City,
            Phone = e.Phone,
            DeptId = e.DeptId
        }).ToList();

        return View(employeeDtos);
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.EmployeeRepo.Add(employee);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        var employee = await _unitOfWork.EmployeeRepo.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.EmployeeRepo.Edit(employee);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        var employee = await _unitOfWork.EmployeeRepo.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var employee = await _unitOfWork.EmployeeRepo.GetById(id);
        if (employee != null)
        {
            _unitOfWork.EmployeeRepo.Delete(employee);
            await _unitOfWork.SaveAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
