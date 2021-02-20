using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HospitalManagementSystem.ViewModel
{
    public class ReceptionistVM
    {
        public List<Receptionist> receptionists { get; set; }
        public Receptionist receptionist { get; set; }
        public List<SelectListItem> recs { get; set; } = new List<SelectListItem>();
    }
}