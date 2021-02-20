using HMSDataLayer;
using HospitalManagementSystem.Models.External_Models;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [HandleError]
    public class MainController : Controller
    {
        SettingsDA SetDA;

        public MainController()
        {
            SetDA = new SettingsDA();
        }

        // GET: Main
        public ActionResult Index()
        {
            Settings set = SetDA.Read();
            ViewBag.HospitalName = set.HospitalName;
            return View();
        }


    }
}