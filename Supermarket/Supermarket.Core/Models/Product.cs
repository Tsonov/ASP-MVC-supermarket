using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarket.Core.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

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

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
