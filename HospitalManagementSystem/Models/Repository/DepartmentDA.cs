using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HMSDataLayer
{
    public class DepartmentDA
    {
        HospitalDBEntities context;

        public DepartmentDA()
        {
            context = new HospitalDBEntities();
        }

        public void Create(Department obj)
        {
            context.Departments.Add(obj);
            context.SaveChanges();
        }

        public List<Department> Read()
        {
            return context.Departments.ToList();
        }

        public void Update(Department obj)
        {
            Department main = context.Departments.Where(a => a.Id == obj.Id).FirstOrDefault();
            main.Name = obj.Name;
            context.SaveChanges();
        }

        public void Delete(Department obj)
        {
            Department main = context.Departments.Where(a => a.Id == obj.Id).FirstOrDefault();
            context.Departments.Remove(main);
            context.SaveChanges();
        }

    }
}
