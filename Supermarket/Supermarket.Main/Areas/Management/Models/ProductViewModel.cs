using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductViewModel
    {
        public int Id { get; internal set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitMeasure { get; set; }

        [Required]
        public decimal Price { get; set; }


        public int CategoryId { get; internal set; }
    }
}
