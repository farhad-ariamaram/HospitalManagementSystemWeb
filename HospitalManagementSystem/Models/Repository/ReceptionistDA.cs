using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HMSDataLayer
{
    public class ReceptionistDA
    {
        HospitalDBEntities context;

        public ReceptionistDA()
        {
            context = new HospitalDBEntities();
        }

        public void Create(Receptionist obj)
        {
            context.Receptionists.Add(obj);
            context.SaveChanges();
        }

        public List<Receptionist> Read()
        {
            return context.Receptionists.ToList();
        }

        public void Update(Receptionist obj)
        {
            Receptionist main = context.Receptionists.Where(a => a.Id == obj.Id).FirstOrDefault();
            main.Name = obj.Name;
            context.SaveChanges();
        }

        public void Delete(Receptionist obj)
        {
            Receptionist main = context.Receptionists.Where(a => a.Id == obj.Id).FirstOrDefault();
            context.Receptionists.Remove(main);
            context.SaveChanges();
        }
    }
}
