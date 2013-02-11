using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class HomeController : AbstractAuthorizedController
    {
        //
        // GET: /Management/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
