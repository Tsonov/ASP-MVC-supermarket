using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    public class Replenishment
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        public virtual ICollection<ReplenishmentDetail> ReplenishmentDetails { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmountPaid { get; set; }
    }
}
