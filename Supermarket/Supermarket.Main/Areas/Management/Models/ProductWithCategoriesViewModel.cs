using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supermarket.Main.Areas.Management.Models
{
    public class ProductWithCategoriesViewModel
    {
        public ProductViewModel ProductModel { get; set; }
        public IEnumerable<CategoryViewModel> AvailableCategories { get; set; }
    }
}