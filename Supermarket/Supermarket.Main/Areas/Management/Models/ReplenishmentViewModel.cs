using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ReplenishmentViewModel
    {
        public DateTime DateAndTime { get; internal set; }

        public decimal TotalAmountPaid { get; set; }

        public IEnumerable<ReplenishmentDetailsViewModel> ProductsLoaded { get; set; }
    }
}