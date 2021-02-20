using HMSDataLayer;
using HospitalManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ReceptionistController : Controller
    {
        ReceptionistDA RecDA;
        PatientDA PatDA;
        DoctorDA DocDA;
        RoomDA RomDA;
        BillDA BilDA;
        BillsHistoryDA PatHisDA;
        static int? CurrentRecId;

        public ReceptionistController()
        {
            RecDA = new ReceptionistDA();
            PatDA = new PatientDA();
            DocDA = new DoctorDA();
            RomDA = new RoomDA();
            BilDA = new BillDA();
            PatHisDA = new BillsHistoryDA();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ci = TempData["ci"];
            var model = new ViewModel.ReceptionistVM();
            if (ViewBag.ci != null)
            {
                CurrentRecId = ViewBag.ci;
                model.receptionist = RecDA.Read().Where(a => a.Id == ViewBag.ci).FirstOrDefault();
            }
            foreach (var item in RecDA.Read())
            {
                model.recs.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            TempData["ci"] = id;
            return RedirectToAction("Index");
        }

        //Reception Section
        [HttpGet]
        public ActionResult Reception()
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                try
                {
                    var model = new ViewModel.PatientVM();
                    model.patients = PatDA.Read().Where(a => a.ReceptionistId == CurrentRecId).ToList();
                    foreach (var item in DocDA.Read())
                    {
                        model.doctors.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    foreach (var item in RomDA.Read())
                    {
                        model.rooms.Add(new SelectListItem { Text = item.Location, Value = item.Id.ToString() });
                    }
                    foreach (var item in RecDA.Read().Where(a => a.Id == CurrentRecId).ToList())
                    {
                        model.receptionists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return View(model);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        [HttpGet]
        public ActionResult CreatePatient()
        {
            return RedirectToAction("Reception");
        }

        [HttpPost]
        public ActionResult CreatePatient(Patient patient)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    PatDA.Create(patient);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Reception");
            }
            else
            {
                return RedirectToAction("Reception");
            }
        }

        [HttpGet]
        public ActionResult DeletePatient(int? id)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (id.HasValue)
            {
                try
                {
                    Patient pat = PatDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (pat == null)
                    {
                        return RedirectToAction("Reception");
                    }
                    try
                    {
                        PatDA.Delete(pat);
                    }
                    catch (Exception)
                    {
                        String error = "این بیمار دارای قبض پرداخت نشده است و نمی‌توانید آن را حذف کنید";
                        return View("Error", null, error);
                    }
                    return RedirectToAction("Reception");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Reception");
            }
        }

        [HttpGet]
        public ActionResult PatientEdit(int? id)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (id.HasValue)
            {
                try
                {
                    var model = new ViewModel.PatientVM();
                    model.patient = PatDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("Reception");
                    }
                    foreach (var item in DocDA.Read())
                    {
                        model.doctors.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    foreach (var item in RomDA.Read())
                    {
                        model.rooms.Add(new SelectListItem { Text = item.Location, Value = item.Id.ToString() });
                    }
                    foreach (var item in RecDA.Read().Where(a => a.Id == CurrentRecId).ToList())
                    {
                        model.receptionists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return View(model);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Reception");
            }

        }

        [HttpPost]
        public ActionResult PatientEdit(Patient patient)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (patient == null)
            {
                return RedirectToAction("Reception");
            }
            else
            {
                try
                {
                    PatDA.Update(patient);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

                return RedirectToAction("Reception");
            }
        }

        //Bill Section
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public ActionResult MakeBill()
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                TempData["BillNo"] = RandomString(7);
                var model = new ViewModel.BillVM();
                model.bills = BilDA.Read().Where(a => a.ReceptionistId == CurrentRecId).ToList();
                foreach (var item in PatDA.Read())
                {
                    model.patients.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                foreach (var item in RecDA.Read().Where(a => a.Id == CurrentRecId).ToList())
                {
                    model.receptionists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                return View(model);
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        [HttpGet]
        public ActionResult CreateBill()
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("MakeBill");
        }

        [HttpPost]
        public ActionResult CreateBill(Bill bill)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    BilDA.Create(bill);
                    return RedirectToAction("MakeBill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("MakeBill");
            }
        }

        [HttpGet]
        public ActionResult DeleteBill(int? id)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (id.HasValue)
            {
                try
                {
                    Bill bil = BilDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (bil == null)
                    {
                        return RedirectToAction("MakeBill");
                    }
                    BilDA.Delete(bil);
                    return RedirectToAction("MakeBill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

            }
            else
            {
                return RedirectToAction("MakeBill");
            }
        }

        [HttpGet]
        public ActionResult BillEdit(int? id)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (id.HasValue)
            {
                try
                {
                    var model = new ViewModel.BillVM();
                    model.bill = BilDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("MakeBill");
                    }
                    foreach (var item in PatDA.Read())
                    {
                        model.patients.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    foreach (var item in RecDA.Read().Where(a => a.Id == CurrentRecId).ToList())
                    {
                        model.receptionists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return View(model);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

            }
            else
            {
                return RedirectToAction("MakeBill");
            }

        }

        [HttpPost]
        public ActionResult BillEdit(Bill bill)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (bill == null)
            {
                return RedirectToAction("MakeBill");
            }
            else
            {
                try
                {
                    BilDA.Update(bill);
                    return RedirectToAction("MakeBill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
        }

        //PayBill Section
        public ActionResult PayBill()
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                List<Bill> model = BilDA.Read().Where(a => a.ReceptionistId == CurrentRecId).ToList();
                return View(model);
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        public ActionResult PayBillConfirm(int? id)
        {
            if (CurrentRecId == null)
            {
                return RedirectToAction("Index");
            }
            if (id.HasValue)
            {
                try
                {
                    Bill bil = BilDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (bil == null)
                    {
                        return RedirectToAction("PayBill");
                    }
                    try
                    {
                        BillsHistory bh = new BillsHistory()
                        {
                            Amount = bil.Amount,
                            BillNo = bil.BillNo,
                            Date = DateTime.Now,
                            PatientName = bil.Patient.Name,
                            ReceptionistName = bil.Receptionist.Name
                        };
                        BilDA.Delete(bil);
                        PatHisDA.Create(bh);
                    }
                    catch (Exception)
                    {
                        String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                        return View("Error", null, error);
                    }
                    return RedirectToAction("PayBill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("PayBill");
            }
        }
    }
}