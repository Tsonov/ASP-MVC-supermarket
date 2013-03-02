using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core.Repositories
{
    public interface IReportsRepository : IDisposable
    {
        IList<ProductAvailabilityDetail> GetProductsInStock();
        IList<ProductAvailabilityDetail> GetProductsInStock(int categoryId);
        IList<ProductAvailabilityDetail> GetProductsInStockFor(DateTime date);
        IList<Category> GetAvailableCategories();
        IList<SaleDetail> GetSales(DateTime start, DateTime end);
    }
}
