using HospitalManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HospitalManagementSystem.ViewModel
{
    public class PatientVM
    {
        public List<Patient> patients { get; set; }
        public Patient patient { get; set; }
        public List<SelectListItem> doctors { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> rooms { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> receptionists { get; set; } = new List<SelectListItem>();
    }
}