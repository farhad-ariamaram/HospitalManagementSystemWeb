using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ViewResult InternalError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}