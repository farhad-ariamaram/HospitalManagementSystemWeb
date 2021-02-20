using HMSDataLayer;
using HospitalManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        DoctorDA DocDA;
        PatientDA PatDA;
        PatientHistoryDA PatHisDA;
        static int? CurrentDocId;

        public DoctorController()
        {
            DocDA = new DoctorDA();
            PatDA = new PatientDA();
            PatHisDA = new PatientHistoryDA();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ci = TempData["ci"];
            var model = new ViewModel.DoctorVM();
            if (ViewBag.ci != null)
            {
                CurrentDocId = ViewBag.ci;
                model.doctor = DocDA.Read().Where(a => a.Id == ViewBag.ci).FirstOrDefault();
            }
            foreach (var item in DocDA.Read())
            {
                model.docs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            TempData["ci"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult Discharge()
        {
            if (CurrentDocId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                List<Patient> model = PatDA.Read().Where(a => a.DoctorId == CurrentDocId).ToList();
                return View(model);
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        public ActionResult DischargePatient(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    Patient pat = PatDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (pat == null)
                    {
                        return RedirectToAction("Discharge");
                    }
                    try
                    {
                        PatientsHistory pho = new PatientsHistory()
                        {
                            Name = pat.Name,
                            Address = pat.Address,
                            Age = pat.Age,
                            Date = DateTime.Now,
                            DoctorName = pat.Doctor.Name,
                            Gender = pat.Gender,
                            PhoneNo = pat.PhoneNo,
                            ReceptionistName = pat.Receptionist.Name,
                            RoomNo = pat.Room.Location
                        };
                        PatDA.Delete(pat);
                        PatHisDA.Create(pho);
                    }
                    catch (Exception)
                    {
                        String error = "این بیمار دارای قبض پرداخت نشده است و نمی‌توانید آن را حذف کنید";
                        return View("Error", null, error);
                    }
                    return RedirectToAction("Discharge");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Discharge");
            }
        }
    }
}