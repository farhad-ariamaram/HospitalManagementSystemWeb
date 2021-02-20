using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace HMSDataLayer
{
    public class BillsHistoryDA
    {
        HospitalDBEntities context;

        public BillsHistoryDA()
        {
            context = new HospitalDBEntities();
        }

        public void Create(BillsHistory obj)
        {
            context.BillsHistories.Add(obj);
            context.SaveChanges();
        }

        public List<BillsHistory> Read()
        {
            return context.BillsHistories.ToList();
        }

    }
}

