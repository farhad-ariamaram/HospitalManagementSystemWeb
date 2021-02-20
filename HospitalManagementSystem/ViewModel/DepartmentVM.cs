using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;

namespace HospitalManagementSystem.ViewModel
{
    public class DepartmentVM
    {
        public List<Department> departments { get; set; }
        public Department department { get; set; }
    }
}