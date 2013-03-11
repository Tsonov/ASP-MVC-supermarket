using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Manufacturer { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 1000000000, ErrorMessage = "The price must be at least {1}")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit")]
        public string UnitMeasure { get; set; }

        public int CategoryId { get; set; }

        [Display(Name="Category")]
        public string CategoryName { get; set; }
    }
}
