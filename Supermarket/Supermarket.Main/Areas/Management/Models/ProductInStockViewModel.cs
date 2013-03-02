using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductInStockViewModel
    {
        [Display(Name="Product")]
        public string ProductName { get; internal set; }

        [Display(Name = "Category")]
        public string CategoryName { get; internal set; }

        [Display(Name = "Amount in stock")]
        public double Amount { get; internal set; }

        [Display(Name = "Price per unit")]
        public decimal PricePerUnit { get; internal set; }

        [Display(Name = "Total selling price")]
        public decimal TotalPrice
        {
            get
            {
                return PricePerUnit * new Decimal(Amount);
            }
        }
    }
}