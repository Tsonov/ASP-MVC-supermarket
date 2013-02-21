using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    [Table("ReplenishmentDetails")]
    public class ReplenishmentDetail
    {
        [Key]
        public int Id { get; private set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public double Amount { get; set; }

        public int ProductReplenishmentId { get; set; }

        public virtual ProductReplenishment ProductReplenishment { get; set; }

        public decimal TotalPaidForProduct { get; set; }
    }
}
