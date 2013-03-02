using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supermarket.Core.Repositories;

namespace Supermarket.Main.DataInfrastructure
{
    public class CashRepository : ICashRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();

        public decimal GetAvailableMoneyAmount()
        {
            decimal result = _context.CashDesk.Single().AvailableAmount;
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}