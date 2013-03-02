using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    [Table("ProductAvailabilities")]
    public class ProductAvailability
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public virtual ICollection<ProductAvailabilityDetail> ProductInfos { get; set; }
    }
}
