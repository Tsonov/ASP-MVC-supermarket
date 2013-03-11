using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Core.Models;

namespace Supermarket.Core.Repositories
{
    public interface ISupermarketItemsRepository : IDisposable
    {
        //Categories
        IList<Category> GetCategories();
        Category GetCategory(int id);
        void AddCategory(Category category);
        void DeleteCategory(int id);
        void UpdateCategory(Category category);
        bool CategoryExists(Category category);
        bool DuplicateNameExists(Category category);

        //Products
        IList<Product> GetProducts();
        Product GetProduct(int id);
        void AddProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(Product product);

        void Save();
    }
}
