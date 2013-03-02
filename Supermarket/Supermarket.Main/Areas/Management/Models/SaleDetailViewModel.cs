using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class SaleDetailViewModel
    {
        public string DateAndTime { get; set; }
        public string ProductName { get; set; }
        public double AmountSold { get; set; }
        public decimal MoneyReceived { get; set; }
    }
}