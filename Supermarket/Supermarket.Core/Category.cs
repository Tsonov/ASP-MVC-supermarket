using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Core
{
    public class Category
    {
        
        public int CategoryId { get; private set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
