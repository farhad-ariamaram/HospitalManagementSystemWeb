using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HospitalManagementSystem.ViewModel
{
    public class DoctorVM
    {
        public List<Doctor> doctors { get; set; }
        public Doctor doctor { get; set; }
        public List<SelectListItem> departments { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> docs { get; set; } = new List<SelectListItem>();
    }
}