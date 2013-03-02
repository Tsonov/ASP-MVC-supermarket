using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;
using Supermarket.Main.Areas.Management.Models;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class ProductController : AbstractManagementAuthorizedController
    {
        private readonly ISupermarketItemsRepository _itemsRepository;

        public ProductController(ISupermarketItemsRepository productRepo)
        {
            _itemsRepository = productRepo;
        }

        //
        // GET: /Management/Product/

        public ActionResult Index()
        {
            var products = _itemsRepository
                .GetProducts()
                .Where(p => p.IsActive == true)
                .Select(product => new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Manufacturer = product.Manufacturer,
                    UnitMeasure = product.UnitMeasure,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name
                });
            return View(products);
        }

        //
        // GET: /Management/Product/Details/5

        public ActionResult Details(int id)
        {
            var product = _itemsRepository.GetProduct(id);
            if (product != null)
            {
                ProductViewModel model = MapProductToModel(product);
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // GET: /Management/Product/Create

        [HttpGet]
        public ActionResult Create()
        {
            ProductWithCategoriesViewModel model = new ProductWithCategoriesViewModel();
            model.AvailableCategories = GetAvailableCategories();
            return View(model);
        }

        //
        // POST: /Management/Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int categoryId, ProductWithCategoriesViewModel model)
        {
            if (_itemsRepository.GetCategory(categoryId) == null)
            {
                ModelState.AddModelError("", "You selected an invalid category!");
            }
            if (ModelState.IsValid)
            {
                model.ProductModel.CategoryId = categoryId;
                Product newProduct = MapModelToProduct(model.ProductModel);
                _itemsRepository.AddProduct(newProduct);
                _itemsRepository.Save();
                return RedirectToAction("Index");
            }
            //Make sure the categories are loaded
            if (model.AvailableCategories == null)
            {
                model.AvailableCategories = GetAvailableCategories();
            }
            //Return to fix the errors
            return View(model);
        }

        //
        // GET: /Management/Product/Edit/5

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductWithCategoriesViewModel model = new ProductWithCategoriesViewModel();
            var product = _itemsRepository.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            model.ProductModel = MapProductToModel(product);
            model.AvailableCategories = GetAvailableCategories();
            return View(model);
        }

        //
        // POST: /Management/Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int categoryId, ProductWithCategoriesViewModel model)
        {
            if (_itemsRepository.GetCategory(categoryId) == null)
            {
                ModelState.AddModelError("", "You selected an invalid category!");
            }
            if (ModelState.IsValid)
            {
                model.ProductModel.CategoryId = categoryId;
                Product updatedProduct = MapModelToProduct(model.ProductModel);
                _itemsRepository.UpdateProduct(updatedProduct);
                _itemsRepository.Save();
                return RedirectToAction("Index");
            }
            //Make sure the categories are loaded
            if (model.AvailableCategories == null)
            {
                model.AvailableCategories = GetAvailableCategories();
            }

            ////Make sure the category is constructed in case someone changed it
            //if (model.ProductModel.Category == null)
            //{
            //    Category productCat =  _itemsRepository.GetProduct(model.ProductModel.Id).Category;
            //    model.ProductModel.Category = new CategoryViewModel() { Id = productCat.Id, Name = productCat.Name };
            //}
            //Return to fix the errors
            return View(model);
        }

        //
        // GET: /Management/Product/Delete/5

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = _itemsRepository.GetProduct(id);
            ProductViewModel model = MapProductToModel(product);
            return View(model);
        }

        //
        // POST: /Management/Product/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection values)
        {
            if (_itemsRepository.GetProduct(id) == null)
            {
                return HttpNotFound();
            }
            else
            {
                _itemsRepository.DeleteProduct(id);
                _itemsRepository.Save();
                return RedirectToAction("Index");
            }
        }

        #region Helpers
        private static ProductViewModel MapProductToModel(Product product)
        {
            ProductViewModel model = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Manufacturer = product.Manufacturer,
                UnitMeasure = product.UnitMeasure,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            };

            return model;
        }

        private static Product MapModelToProduct(ProductViewModel model, bool isActive = true)
        {
            Product result = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Manufacturer = model.Manufacturer,
                Price = model.Price,
                UnitMeasure = model.UnitMeasure,
                CategoryId = model.CategoryId,
                IsActive = isActive,
            };
            return result;
        }

        private IEnumerable<CategoryViewModel> GetAvailableCategories()
        {
            var result = _itemsRepository
                .GetCategories()
                .Select(cat => new CategoryViewModel() { Id = cat.Id, Name = cat.Name });
            return result;
        }
        #endregion
    }
}
