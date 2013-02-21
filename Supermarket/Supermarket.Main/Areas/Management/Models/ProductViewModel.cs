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
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit")]
        public string UnitMeasure { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
