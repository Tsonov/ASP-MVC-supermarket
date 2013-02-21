using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Core.Models
{
    public class Category
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public bool CanBeDeleted
        {
            get
            {
                if (this.Products == null ||
                    this.Products.Where(p => p.IsActive == true).Count() == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
