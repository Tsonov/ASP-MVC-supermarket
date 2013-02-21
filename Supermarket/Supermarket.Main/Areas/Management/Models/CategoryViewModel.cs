using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Category name")]
        public string Name { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }

        public bool CanBeDeleted { get; set; }
    }
}