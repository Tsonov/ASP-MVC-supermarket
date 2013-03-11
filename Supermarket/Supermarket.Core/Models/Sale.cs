using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarket.Core.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; private set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public decimal TotalAmountPaid { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
