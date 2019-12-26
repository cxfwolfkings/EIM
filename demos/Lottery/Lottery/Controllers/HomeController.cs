using System.Web.Mvc;

namespace Lottery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            //return View();
            return RedirectToAction("Index", "Lottery");
        }
    }
}
