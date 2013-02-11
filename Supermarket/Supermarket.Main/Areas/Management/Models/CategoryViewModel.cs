using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class CategoryViewModel
    {
        public int Id { get; internal set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IQueryable<ProductViewModel> Products { get; set; }
    }
}