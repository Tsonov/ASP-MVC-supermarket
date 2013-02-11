using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Core.Models
{
    [Table("HistoryProductInfos")]
    public class HistoryProductInfo
    {
        [Key]
        public int Id { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductHistoryId { get; set; }

        public virtual ProductHistory ProductHistory { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }
    }
}
