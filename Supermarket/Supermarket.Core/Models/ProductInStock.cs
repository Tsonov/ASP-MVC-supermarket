using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    [Table("ProductsInStock")]
    public class ProductInStock
    {
        [Key]
        public int Id { get; private set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }
    }
}
