using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarket.Core.Models
{
    [Table("SaleDetails")]
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        public double Amount { get; set; }

        public decimal PricePerUnit { get; set; }

        [NotMapped]
        public decimal TotalMoney
        {
            get
            {
                return new Decimal(Amount) * PricePerUnit;
            }
        }
    }
}
