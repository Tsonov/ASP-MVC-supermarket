using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supermarket.Core.Repositories;
using Supermarket.Core.Models;

namespace Supermarket.Main.DataInfrastructure
{
    public class SupermarketItemsRepository : ISupermarketItemsRepository
    {
        private readonly SupermarketDB _context = new SupermarketDB();

        #region Categories
        public IQueryable<Category> GetCategories()
        {
            var categories = _context.Categories.Where(cat => cat.IsActive == true);
            return categories;
        }

        public bool DuplicateNameExists(Category category)
        {
            var possibleDuplicates = _context.Categories
                .AsNoTracking()
                .Where(cat => cat.IsActive == true
                    && cat.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase)
                    && cat.Id != category.Id);
            bool result = possibleDuplicates.Count() > 0;
            return result;
        }

        public bool CategoryExists(Category category)
        {
            bool result = _context.Categories
                .AsNoTracking()
                .Where(cat => cat.IsActive == true)
                .SingleOrDefault(cat => cat.Name.Equals(category.Name, StringComparison.InvariantCultureIgnoreCase)) != null;
            return result;
        }

        public Category GetCategory(int id)
        {
            Category cat = GetSingleActiveCategoryOrNull(id);
            if (cat == null)
            {
                return null;
            }
            else
            {
                Category catWithoutInactive = new Category() { Id = cat.Id, Name = cat.Name, Products = cat.Products.Where(p => p.IsActive == true).ToArray() };
                return catWithoutInactive;
            }
        }

        public void AddCategory(Category category)
        {
            category.IsActive = true;
            _context.Categories.Add(category);
        }

        public void DeleteCategory(int id)
        {
            Category cat = GetSingleActiveCategoryOrNull(id);
            if (cat != null)
            {
                //Keep the category as inactive for reporting
                cat.IsActive = false;
            }
            else
            {
                throw new InvalidOperationException("The category for deletion does not exist or can not be used");
            }
        }

        public void UpdateCategory(Category category)
        {
            Category cat = GetSingleActiveCategoryOrNull(category.Id);
            if (cat != null)
            {
                cat.Name = category.Name;
                cat.Products = category.Products;
            }
            else
            {
                throw new InvalidOperationException("The category for edit does not exist or can not be used");
            }
        }

        private Category GetSingleActiveCategoryOrNull(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id && c.IsActive == true);
        }

        #endregion

        #region Products

        public IQueryable<Product> GetProducts()
        {
            var products = _context.Products.Where(p => p.IsActive == true);
            return products;
        }

        public Product GetProduct(int id)
        {
            return GetSingleActiveProductOrNull(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = GetSingleActiveProductOrNull(id);
            if (product != null)
            {
                //Keep the product in the database as inactive for reports
                product.IsActive = false;
            }
            else
            {
                throw new InvalidOperationException("The product for deletion does not exist or is not in use");
            }
        }

        public void UpdateProduct(Product product)
        {
            var oldProduct = GetSingleActiveProductOrNull(product.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                Category cat = GetSingleActiveCategoryOrNull(product.CategoryId);
                if (cat == null)
                {
                    //Somehow an invalid category was selected
                    throw new InvalidOperationException("Invalid category provided for the product");
                }
                oldProduct.Category = cat;
                oldProduct.Manufacturer = product.Manufacturer;
                oldProduct.Price = product.Price;
                oldProduct.UnitMeasure = product.UnitMeasure;
            }
            else
            {
                throw new InvalidOperationException("The product for edit does not exist or is not in use");
            }
        }

        private Product GetSingleActiveProductOrNull(int id)
        {
            return _context.Products.SingleOrDefault(p => p.Id == id && p.IsActive == true);
        }

        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}