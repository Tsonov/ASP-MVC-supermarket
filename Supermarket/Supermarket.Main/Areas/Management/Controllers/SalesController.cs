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
    public class SalesController : AbstractManagementAuthorizedController
    {
        private readonly ISalesRepository _salesRepo;

        public SalesController(ISalesRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        //
        // GET: /Management/Sales/MakeSale

        [HttpGet]
        public ActionResult MakeSale()
        {
            ProductOperationDetailsViewModel model = new ProductOperationDetailsViewModel();
            model.AvailableProducts = _salesRepo.GetAvailableProducts().Select(product => new SelectListItem() { Text = product.Name, Value = product.Id.ToString() });
            model.AvailableProducts.First().Selected = true;
            return View(model);
        }

        //
        // POST: /Management/Sales/PerformSale

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult PerformSale(List<ProductOperationDetailsViewModel> sales)
        {
            if (ModelState.IsValid &&
                sales != null &&
                sales.Count > 0)
            {
                decimal totalAmountToRecieve = 0;
                foreach (var sale in sales)
                {
                    decimal priceInStore = _salesRepo.GetProduct(sale.ProductId).Price;
                    if (priceInStore != sale.PricePerUnit)
                    {
                        return new JsonResult()
                        {
                            Data = new { success = false, error = "The price for one or more of the products does not match the store price, please review the sale" }
                        };
                    }
                    totalAmountToRecieve += sale.PricePerUnit;
                }
                try
                {
                    var salesEntities = sales.Select(s => new SaleDetail() { ProductId = s.ProductId, Amount = s.Amount, PricePerUnit = s.PricePerUnit });
                    _salesRepo.MakeSale(salesEntities);
                    _salesRepo.Save();
                    return new JsonResult()
                    {
                        Data = new { success = true, message = "The transaction was successful" }
                    };
                }
                catch (DbUpdateException)
                {
                    return new JsonResult()
                    {
                        Data = new { success = false, error = "A product you are trying to sell has gone out of stock" }
                    };
                }
            }
            else
            {
                return new JsonResult()
                {
                    Data = new { success = false, error = "The list of sales is invalid!" }
                };
            }
        }

        //
        // GET: /Management/Sales/GetProductPrice

        [HttpGet]
        public JsonResult GetProductPrice(int productId)
        {
            var result = _salesRepo.GetProduct(productId);
            return Json(new { price = result.Price }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Management/Sales/GetAmountInStock

        [HttpGet]
        public JsonResult GetAmountInStock(int productId)
        {
            var result = _salesRepo.GetAvailableAmount(productId);
            return Json(new { amount = result }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _salesRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
