using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    [Table("CashDesk")]
    public class CashDesk
    {
        [Key]
        public int Id { get; set; }

        public decimal AvailableAmount { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
}
