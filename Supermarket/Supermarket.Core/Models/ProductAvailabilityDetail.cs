using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarket.Core.Models
{
    [Table("ProductAvailabilitiesDetails")]
    public class ProductAvailabilityDetail
    {
        [Key]
        public int Id { get; private set; }

        public int ProductAvailabilityId { get; set; }

        public virtual ProductAvailability ProductAvailability { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
