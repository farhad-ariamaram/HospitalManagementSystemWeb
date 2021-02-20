using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HMSDataLayer
{
    public class PatientHistoryDA
    {
        HospitalDBEntities context;

        public PatientHistoryDA()
        {
            context = new HospitalDBEntities();
        }

        public void Create(PatientsHistory obj)
        {
            context.PatientsHistories.Add(obj);
            context.SaveChanges();
        }

        public List<PatientsHistory> Read()
        {
            return context.PatientsHistories.ToList();
        }
    }
}
