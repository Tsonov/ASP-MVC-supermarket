using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Core.Repositories;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class HomeController : AbstractManagementAuthorizedController
    {
        private readonly ICashRepository _cashRepo;


        public HomeController(ICashRepository cashRepo)
        {
            this._cashRepo = cashRepo;
        }
            
        //
        // GET: /Management/Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCashAmount()
        {
            decimal availableMoney = _cashRepo.GetAvailableMoneyAmount();
            return Json(new { availableMoney = availableMoney }, JsonRequestBehavior.AllowGet);
        }
    }
}
