using System.Web.Mvc;

namespace timney_photos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Random()
        {
            return View();
        }
    }
}