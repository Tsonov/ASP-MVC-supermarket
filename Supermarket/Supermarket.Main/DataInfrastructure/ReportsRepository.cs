using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;

namespace Supermarket.Main.DataInfrastructure
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();

        public IList<SaleDetail> GetSales(DateTime start, DateTime end)
        {
            DateTime startNeutral = start.Date;
            DateTime endNeutral = end.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var sales = _context.Sales
                            .Where(s => startNeutral.CompareTo(s.DateAndTime) <= 0 && s.DateAndTime.CompareTo(endNeutral) <= 0)
                            .ToList();
            var result = new List<SaleDetail>();
            foreach (var sale in sales)
            {
                result.AddRange(sale.SaleDetails);
            }
            return result;
        }

        public IList<Category> GetAvailableCategories()
        {
            var categories = _context.Categories.AsNoTracking().Where(cat => cat.IsActive == true).ToList();
            return categories;
        }

        public IList<ProductAvailabilityDetail> GetProductsInStockFor(DateTime date)
        {
            var availability = _context.ProductAvailabilities
                .OrderByDescending(x => x.Date)
                .FirstOrDefault(x => x.Date.CompareTo(date) <= 0);
            if (availability == null)
            {
                return null;
            }
            else
            {
                var result = availability.ProductInfos.Where(x => x.Product.IsActive == true).ToList();
                return result;
            }
        }

        public IList<ProductAvailabilityDetail> GetProductsInStock(int categoryId)
        {
            var currentProductAvailability = GetCurrentProductAvailability();
            if (currentProductAvailability == null)
            {
                return null;
            }
            else
            {
                var result = currentProductAvailability.ProductInfos
                    .Where(p => p.Product.IsActive == true && p.Product.CategoryId == categoryId)
                    .ToList();
                return result;
            }
        }

        public IList<ProductAvailabilityDetail> GetProductsInStock()
        {
            var currentProductAvailability = GetCurrentProductAvailability();
            if (currentProductAvailability == null)
            {
                return null;
            }
            else
            {
                var result = currentProductAvailability.ProductInfos.Where(x => x.Product.IsActive == true).ToList();
                return result;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private ProductAvailability GetCurrentProductAvailability()
        {
            var currentProductAvailability = _context.ProductAvailabilities.OrderByDescending(x => x.Date).FirstOrDefault();
            if (currentProductAvailability.Date.CompareTo(DateTime.Now.Date) > 0)
            {
                throw new InvalidOperationException("The database is in an invalid state. Please contact an administrator");
            }
            return currentProductAvailability;
        }
    }
}