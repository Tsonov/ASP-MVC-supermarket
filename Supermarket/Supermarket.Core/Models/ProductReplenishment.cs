using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    public class ProductReplenishment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public DateTime TimeOfReplenishment { get; set; }

        public virtual ICollection<ReplenishmentDetail> ReplenishmentDetail { get; set; }

        public decimal TotalAmountPaid { get; set; }
    }
}
