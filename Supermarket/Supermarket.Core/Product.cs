using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core
{
    public class Product
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string UnitMeasure { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
    }
}
