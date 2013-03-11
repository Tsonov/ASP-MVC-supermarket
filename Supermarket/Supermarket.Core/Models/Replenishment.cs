using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Supermarket.Core.Models
{
    public class Replenishment
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public virtual ICollection<ReplenishmentDetail> ReplenishmentDetails { get; set; }

        [Range(0, 1000000000)]
        public decimal TotalAmountPaid { get; set; }
    }
}
