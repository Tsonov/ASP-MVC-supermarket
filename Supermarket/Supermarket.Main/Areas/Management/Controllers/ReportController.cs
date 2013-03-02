using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;
using Supermarket.Main.Areas.Management.Models;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class ReportController : AbstractManagementAuthorizedController
    {
        private readonly IReportsRepository _reportsRepo;

        public ReportController(IReportsRepository reportsRepo)
        {
            _reportsRepo = reportsRepo;
        }

        //
        // GET: /Management/Reports/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Management/Reports/ProductsInStock

        [HttpGet]
        public ActionResult ProductsInStock()
        {
            var availabilities = _reportsRepo.GetProductsInStock();
            var availabilitiesViewModel = MapAvailabilitiesToViewModel(availabilities);
            return View(availabilitiesViewModel);
        }

        //
        // GET: /Management/Reports/GetProductsInStockForCategory

        [HttpGet]
        public JsonResult GetProductsInStock()
        {
            var availabilities = _reportsRepo.GetProductsInStock();
            var availabilitiesViewModel = MapAvailabilitiesToViewModel(availabilities);
            return Json(availabilitiesViewModel, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Management/Reports/GetProductsInStockForCategory

        [HttpGet]
        public JsonResult GetProductsInStockForCategory(int categoryId)
        {
            var availabilities = _reportsRepo.GetProductsInStock(categoryId);
            var availabilitiesViewModel = MapAvailabilitiesToViewModel(availabilities);
            return Json(availabilitiesViewModel, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Management/Reports/GetCategories

        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = _reportsRepo.GetAvailableCategories();
            var jsonResult = categories.Select(cat => new { CategoryName = cat.Name, CategoryId = cat.Id });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Management/Reports/ProductsInStockByDate

        [HttpGet]
        public ActionResult ProductsInStockByDate()
        {
            return View();
        }

        //
        // GET: /Management/Reports/GetAvailabilitiesByDate

        [HttpGet]
        public ActionResult GetAvailabilitiesByDate(DateTime date)
        {
            if (date != null)
            {
                var availabilities = _reportsRepo.GetProductsInStockFor(date);
                if (availabilities == null)
                {
                    return Json(new { success = true, data = new { } }, JsonRequestBehavior.AllowGet);
                }
                List<ProductInStockViewModel> viewModel = MapAvailabilitiesToViewModel(availabilities);
                return Json(new { success =  true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new JsonResult()
                {
                    Data = new { success = false, error = "Invalid date given" }
                };
            }
        }

        //
        // GET: /Management/Reports/Sales

        [HttpGet]
        public ActionResult Sales()
        {
            var sales = _reportsRepo.GetSales(DateTime.Now.AddDays(-7), DateTime.Now);
            var viewModel = MapSalesToViewModel(sales);
            return View(viewModel);
        }
        //
        // GET: /Management/Reports/SalesInPeriod

        [HttpGet]
        public JsonResult SalesInPeriod(DateTime start, DateTime end)
        {
            if (start != null && 
                end != null &&
                start.Date.CompareTo(end.Date) <= 0)
            {
                var sales = _reportsRepo.GetSales(start, end);
                if (sales == null)
                {
                    return Json(new { success = true, data = new { } }, JsonRequestBehavior.AllowGet);
                }
                List<SaleDetailViewModel> viewModel = MapSalesToViewModel(sales);
                return Json(new { success = true, data = viewModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, error = "Invalid dates given" }, JsonRequestBehavior.AllowGet);
            }
        }


        protected override void Dispose(bool disposing)
        {
            _reportsRepo.Dispose();
            base.Dispose(disposing);
        }

        #region Helpers

        private static List<SaleDetailViewModel> MapSalesToViewModel(IList<SaleDetail> sales)
        {
            List<SaleDetailViewModel> viewModel = sales.Select(s => new SaleDetailViewModel()
            {
                DateAndTime = s.Sale.DateAndTime.ToString(),
                ProductName = s.Product.Name,
                AmountSold = s.Amount,
                MoneyReceived = s.TotalMoney
            }).ToList();
            return viewModel;
        }

        private List<ProductInStockViewModel> MapAvailabilitiesToViewModel(IList<ProductAvailabilityDetail> entities)
        {
            List<ProductInStockViewModel> result = new List<ProductInStockViewModel>();
            foreach (var productAvailabilityDetail in entities)
            {
                result.Add(new ProductInStockViewModel()
                {
                    Amount = productAvailabilityDetail.Amount,
                    ProductName = productAvailabilityDetail.Product.Name,
                    PricePerUnit = productAvailabilityDetail.Product.Price,
                    CategoryName = productAvailabilityDetail.Product.Category.Name
                });
            }
            return result;
        }
        #endregion
    }
}
