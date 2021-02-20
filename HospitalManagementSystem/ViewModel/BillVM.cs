using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HospitalManagementSystem.ViewModel
{
    public class BillVM
    {
        public List<Bill> bills { get; set; }
        public Bill bill { get; set; }
        public List<SelectListItem> receptionists { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> patients { get; set; } = new List<SelectListItem>();
    }
}