using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core.Repositories
{
    public interface IReplenishmentRepository : ICashRepository
    {
        IEnumerable<Product> GetAvailableProducts();
        bool EnoughMoneyInCashDeskForPayment(decimal amountToPay);
        void MakeReplenishment(IEnumerable<ReplenishmentDetail> replenishments);
        void Save();
    }
}
