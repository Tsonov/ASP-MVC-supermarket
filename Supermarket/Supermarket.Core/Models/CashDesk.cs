using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarket.Core.Models
{
    [Table("CashDesk")]
    public class CashDesk
    {
        [Key]
        public int Id { get; set; }

        public decimal AvailableAmount { get; set; }
    }
}
