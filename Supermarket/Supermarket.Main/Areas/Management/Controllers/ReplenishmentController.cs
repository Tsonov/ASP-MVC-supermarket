using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Main.Areas.Management.Models;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class ReplenishmentController : AbstractAuthorizedController
    {
        //
        // GET: /Management/Replenishment/

        [HttpGet]
        public ActionResult LoadProducts()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadProducts(IEnumerable<ReplenishmentDetailsViewModel> models)
        {
            return View();
        }

    }
}
