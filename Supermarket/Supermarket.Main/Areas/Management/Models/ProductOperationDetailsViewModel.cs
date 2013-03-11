using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductOperationDetailsViewModel
    {
        public IEnumerable<SelectListItem> AvailableProducts { get; internal set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "You must enter replenished amount for this product")]
        [Range(Double.Epsilon, double.MaxValue, ErrorMessage = "The amount must be positive")]
        public double Amount { get; set; }

        [Display(Name = "Price per unit")]
        [Required(ErrorMessage = "You must enter a price for an unit of this product")]
        [Range(0.01, 1000000000, ErrorMessage = "The price must be a at least {1}")]
        [DataType(DataType.Currency)]
        public decimal PricePerUnit { get; set; }

    }
}