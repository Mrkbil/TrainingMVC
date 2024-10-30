using System.Collections.Generic;
using TrainingMVC.Model.Entity;

namespace TrainingMVC.Model
{
    public class HomeViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Designation> Designations { get; set; }
    }
}
