using System;
using System.Linq;
using System.Web.Mvc;

namespace Supermarket.Main.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your solution for fast and easy supermarket management.";

            return View();
        }
    }
}
