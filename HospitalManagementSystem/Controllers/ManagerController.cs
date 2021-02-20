using HMSDataLayer;
using HospitalManagementSystem.Models.Domain;
using HospitalManagementSystem.Models.External_Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        DepartmentDA DepDA;
        DoctorDA DocDA;
        PatientDA PatDA;
        RoomDA RomDA;
        ReceptionistDA RecDA;
        BillDA BilDA;
        SettingsDA SetDA;

        public ManagerController()
        {
            DepDA = new DepartmentDA();
            DocDA = new DoctorDA();
            PatDA = new PatientDA();
            RomDA = new RoomDA();
            RecDA = new ReceptionistDA();
            BilDA = new BillDA();
            SetDA = new SettingsDA();
        }

        // GET: Manager
        public ActionResult Index()
        {
            Settings set = SetDA.Read();
            ViewBag.HospitalName = set.HospitalName;
            ViewBag.ManagerName = set.ManagerName;
            return View();
        }

        // Department Section

        [HttpGet]
        public ActionResult Department()
        {
            var model = new ViewModel.DepartmentVM();
            try
            {
                model.departments = DepDA.Read();
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateDepartment()
        {
            return RedirectToAction("Department");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DepDA.Create(department);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Department");
            }
            else
            {
                return RedirectToAction("Department");
            }
        }

        [HttpGet]
        public ActionResult DeleteDepartment(int? id)
        {
            if (id.HasValue)
            {
                Department dep = DepDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                if (dep == null)
                {
                    return RedirectToAction("Department");
                }
                try
                {
                    DepDA.Delete(dep);
                }
                catch (Exception)
                {
                    String error = "این ساختمان دارای پزشک است و نمی‌توانید آن را حذف کنید";
                    return View("Error", null, error);
                }

                return RedirectToAction("Department");
            }
            else
            {
                return RedirectToAction("Department");
            }
        }

        [HttpGet]
        public ActionResult DepartmentEdit(int? id)
        {
            if (id.HasValue)
            {
                var model = new Department();
                try
                {
                    model = DepDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                if (model == null)
                {
                    return RedirectToAction("Department");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Department");
            }

        }

        [HttpPost]
        public ActionResult DepartmentEdit(Department department)
        {
            if (department == null)
            {
                return RedirectToAction("Department");
            }
            else
            {
                try
                {
                    DepDA.Update(department);
                }
                catch (Exception)
                {

                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

                return RedirectToAction("Department");
            }
        }

        //Doctor Section
        [HttpGet]
        public ActionResult Doctor()
        {
            var model = new ViewModel.DoctorVM();
            try
            {
                model.doctors = DocDA.Read();
                foreach (var item in DepDA.Read())
                {
                    model.departments.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateDoctor()
        {
            return RedirectToAction("Doctor");
        }

        [HttpPost]
        public ActionResult CreateDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DocDA.Create(doctor);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Doctor");
            }
            else
            {
                return RedirectToAction("Doctor");
            }
        }

        [HttpGet]
        public ActionResult DeleteDoctor(int? id)
        {
            if (id.HasValue)
            {
                Doctor doc;
                try
                {
                    doc = DocDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                if (doc == null)
                {
                    return RedirectToAction("Doctor");
                }
                try
                {
                    DocDA.Delete(doc);
                }
                catch (Exception)
                {
                    String error = "این پزشک دارای بیمار ترخیص نشده است و نمی‌توانید آن را حذف کنید";
                    return View("Error", null, error);
                }
                return RedirectToAction("Doctor");
            }
            else
            {
                return RedirectToAction("Doctor");
            }
        }

        [HttpGet]
        public ActionResult DoctorEdit(int? id)
        {
            if (id.HasValue)
            {
                var model = new ViewModel.DoctorVM();
                try
                {
                    model.doctor = DocDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

                if (model == null)
                {
                    return RedirectToAction("Doctor");
                }
                try
                {
                    foreach (var item in DepDA.Read())
                    {
                        model.departments.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Doctor");
            }

        }

        [HttpPost]
        public ActionResult DoctorEdit(Doctor doctor)
        {
            if (doctor == null)
            {
                return RedirectToAction("Doctor");
            }
            else
            {
                try
                {
                    DocDA.Update(doctor);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Doctor");
            }
        }

        //Patient Section
        [HttpGet]
        public ActionResult Patient()
        {
            try
            {
                var model = new ViewModel.PatientVM();
                model.patients = PatDA.Read();
                foreach (var item in DocDA.Read())
                {
                    model.doctors.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                foreach (var item in RomDA.Read())
                {
                    model.rooms.Add(new SelectListItem { Text = item.Location, Value = item.Id.ToString() });
                }
                foreach (var item in RecDA.Read())
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
        public ActionResult CreatePatient()
        {
            return RedirectToAction("Patient");
        }

        [HttpPost]
        public ActionResult CreatePatient(Patient patient)
        {
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
                return RedirectToAction("Patient");
            }
            else
            {
                return RedirectToAction("Patient");
            }
        }

        [HttpGet]
        public ActionResult DeletePatient(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    Patient pat = PatDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (pat == null)
                    {
                        return RedirectToAction("Patient");
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
                    return RedirectToAction("Patient");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Patient");
            }
        }

        [HttpGet]
        public ActionResult PatientEdit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var model = new ViewModel.PatientVM();
                    model.patient = PatDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("Patient");
                    }
                    foreach (var item in DocDA.Read())
                    {
                        model.doctors.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    foreach (var item in RomDA.Read())
                    {
                        model.rooms.Add(new SelectListItem { Text = item.Location, Value = item.Id.ToString() });
                    }
                    foreach (var item in RecDA.Read())
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
                return RedirectToAction("Patient");
            }

        }

        [HttpPost]
        public ActionResult PatientEdit(Patient patient)
        {
            if (patient == null)
            {
                return RedirectToAction("Patient");
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

                return RedirectToAction("Patient");
            }
        }

        // Room Section

        [HttpGet]
        public ActionResult Room()
        {
            try
            {
                var model = new ViewModel.RoomVM();
                model.rooms = RomDA.Read();
                return View(model);
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        [HttpGet]
        public ActionResult CreateRoom()
        {
            return RedirectToAction("Room");
        }

        [HttpPost]
        public ActionResult CreateRoom(Room room)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RomDA.Create(room);
                    return RedirectToAction("Room");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Room");
            }
        }

        [HttpGet]
        public ActionResult DeleteRoom(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    Room rom = RomDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (rom == null)
                    {
                        return RedirectToAction("Room");
                    }
                    try
                    {
                        RomDA.Delete(rom);
                    }
                    catch (Exception)
                    {
                        String error = "این اتاق دارای بیمار ترخیص نشده است و نمی‌توانید آن را حذف کنید";
                        return View("Error", null, error);
                    }
                    return RedirectToAction("Room");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Room");
            }
        }

        [HttpGet]
        public ActionResult RoomEdit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var model = new Room();
                    model = RomDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("Room");
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
                return RedirectToAction("Room");
            }

        }

        [HttpPost]
        public ActionResult RoomEdit(Room room)
        {
            if (room == null)
            {
                return RedirectToAction("Room");
            }
            else
            {
                try
                {
                    RomDA.Update(room);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Room");
            }
        }

        // Receptionist Section
        [HttpGet]
        public ActionResult Receptionist()
        {
            try
            {
                var model = new ViewModel.ReceptionistVM();
                model.receptionists = RecDA.Read();
                return View(model);
            }
            catch (Exception)
            {
                String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                return View("Error", null, error);
            }
        }

        [HttpGet]
        public ActionResult CreateReceptionist()
        {
            return RedirectToAction("Receptionist");
        }

        [HttpPost]
        public ActionResult CreateReceptionist(Receptionist receptionist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RecDA.Create(receptionist);
                    return RedirectToAction("Receptionist");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Receptionist");
            }
        }

        [HttpGet]
        public ActionResult DeleteReceptionist(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    Receptionist rec = RecDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (rec == null)
                    {
                        return RedirectToAction("Receptionist");
                    }
                    try
                    {
                        RecDA.Delete(rec);
                    }
                    catch (Exception)
                    {
                        String error = "این پذیرش دارای بیمار ترخیص نشده است و نمی‌توانید آن را حذف کنید";
                        return View("Error", null, error);
                    }
                    return RedirectToAction("Receptionist");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Receptionist");
            }
        }

        [HttpGet]
        public ActionResult ReceptionistEdit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var model = new Receptionist();
                    model = RecDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("Receptionist");
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
                return RedirectToAction("Receptionist");
            }

        }

        [HttpPost]
        public ActionResult ReceptionistEdit(Receptionist receptionist)
        {
            if (receptionist == null)
            {
                return RedirectToAction("Receptionist");
            }
            else
            {
                try
                {
                    RecDA.Update(receptionist);
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
                return RedirectToAction("Receptionist");
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

        [HttpGet]
        public ActionResult Bill()
        {
            try
            {
                TempData["BillNo"] = RandomString(7);
                var model = new ViewModel.BillVM();
                model.bills = BilDA.Read();
                foreach (var item in PatDA.Read())
                {
                    model.patients.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                foreach (var item in RecDA.Read())
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
            return RedirectToAction("Bill");
        }

        [HttpPost]
        public ActionResult CreateBill(Bill bill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BilDA.Create(bill);
                    return RedirectToAction("Bill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
            else
            {
                return RedirectToAction("Bill");
            }
        }

        [HttpGet]
        public ActionResult DeleteBill(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    Bill bil = BilDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).Single();
                    if (bil == null)
                    {
                        return RedirectToAction("Bill");
                    }
                    BilDA.Delete(bil);
                    return RedirectToAction("Bill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }

            }
            else
            {
                return RedirectToAction("Bill");
            }
        }

        [HttpGet]
        public ActionResult BillEdit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var model = new ViewModel.BillVM();
                    model.bill = BilDA.Read().Where(a => a.Id == id).DefaultIfEmpty(null).First();
                    if (model == null)
                    {
                        return RedirectToAction("Bill");
                    }
                    foreach (var item in PatDA.Read())
                    {
                        model.patients.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                    foreach (var item in RecDA.Read())
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
                return RedirectToAction("Bill");
            }

        }

        [HttpPost]
        public ActionResult BillEdit(Bill bill)
        {
            if (bill == null)
            {
                return RedirectToAction("Bill");
            }
            else
            {
                try
                {
                    BilDA.Update(bill);
                    return RedirectToAction("Bill");
                }
                catch (Exception)
                {
                    String error = "در هنگام ارتباط با پایگاه داده مشکل بوجود آمده";
                    return View("Error", null, error);
                }
            }
        }

        //Settings Section
        [HttpGet]
        public ActionResult Settings()
        {
            Settings set = SetDA.Read();
            ViewBag.status = TempData["status"];
            return View(set);
        }

        [HttpPost]
        public ActionResult Settings(Settings set)
        {
            if (ModelState.IsValid)
            {
                SetDA.Write(set);
                TempData["status"] = "<script>alert('با موفقیت ثبت شد');</script>";
            }
            else
            {
                TempData["status"] = "<script>alert('خطایی رخ داده است');</script>";
            }
            return RedirectToAction("Settings");

        }
    }
}