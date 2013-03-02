using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core.Repositories
{
    public interface ISalesRepository : IDisposable
    {
        IEnumerable<Product> GetAvailableProducts();
        void MakeSale(IEnumerable<SaleDetail> saleDetails);
        decimal GetStorePriceFor(int productId);
        void Save();
    }
}
