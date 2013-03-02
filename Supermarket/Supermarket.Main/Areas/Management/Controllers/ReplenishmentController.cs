using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;
using Supermarket.Main.Areas.Management.Models;
using Supermarket.Main.Attributes;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class ReplenishmentController : AbstractManagementAuthorizedController
    {
        private readonly IReplenishmentRepository _replenishRepo;

        public ReplenishmentController(IReplenishmentRepository replenishRepo)
        {
            _replenishRepo = replenishRepo;
        }

        //
        // GET: /Management/Replenishment/LoadProducts

        [HttpGet]
        public ActionResult LoadProducts()
        {
            ProductOperationDetailsViewModel model = new ProductOperationDetailsViewModel();
            model.AvailableProducts = _replenishRepo.GetAvailableProducts().Select(product => new SelectListItem() { Text = product.Name, Value = product.Id.ToString() });
            return View(model);
        }


        //
        // POST: /Management/Replenishment/AddReplenishment
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult AddReplenishment(List<ProductOperationDetailsViewModel> replenishments)
        {
            if (ModelState.IsValid &&
                replenishments != null &&
                replenishments.Count > 0)
            {
                decimal totalAmountToPay = replenishments.Sum(r => r.PricePerUnit * new Decimal(r.Amount));
                if (_replenishRepo.EnoughMoneyInCashDeskForPayment(totalAmountToPay))
                {
                    try
                    {
                        var replenishmentsEntities = replenishments.Select(r => new ReplenishmentDetail() { ProductId = r.ProductId, Amount = r.Amount, PricePerUnit = r.PricePerUnit });
                        _replenishRepo.MakeReplenishment(replenishmentsEntities);
                        _replenishRepo.Save();
                        return new JsonResult()
                        {
                            Data = new { success = true, message = "The transaction was successful" }
                        };
                    }
                    catch (DbUpdateException)
                    {
                        return new JsonResult()
                        {
                            Data = new { success = false, error = "An error has occurred while saving the replenishment. Please try to resend." }
                        };
                    }
                }
                else
                {
                    decimal availableAmount = _replenishRepo.GetAvailableMoneyAmount();
                    string error = "There were not enough money to make the transaction. There are currently " +
                        availableAmount +
                        " in the cash desk. You want to make a replenishment for " + totalAmountToPay + ". Please review the replenishment list.";
                    return new JsonResult()
                    {
                        Data = new { success = false, error = error }
                    };
                }
            }
            else
            {
                return new JsonResult()
                {
                    Data = new { success = false, error = "The list of replenishments is invalid!" }
                };
            }
        }

        protected override void Dispose(bool disposing)
        {
            _replenishRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
