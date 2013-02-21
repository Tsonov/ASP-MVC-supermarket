using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ReplenishmentDetailsViewModel
    {
        public int ProductId { get; set; }

        public double Amount { get; set; }

        public decimal TotalPaidForProduct { get; set; }
    }
}