using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermarket.Core.Models;
using Supermarket.Core.Repositories;
using Supermarket.Main.Areas.Management.Models;

namespace Supermarket.Main.Areas.Management.Controllers
{
    public class CategoryController : AbstractAuthorizedController
    {

        private readonly ISupermarketItemsRepository _itemsRepository;

        public CategoryController(ISupermarketItemsRepository itemsRepo)
        {
            _itemsRepository = itemsRepo;
        }

        //
        // GET: /Management/Category/

        public ActionResult Index()
        {
            var categories = _itemsRepository.GetCategories().ToArray();
            List<CategoryViewModel> categoriesModels = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                categoriesModels.Add(new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CanBeDeleted = category.CanBeDeleted
                });
            }
            return View(categoriesModels);
        }

        //
        // GET: /Management/Category/Details/5

        public ActionResult Details(int id)
        {
            var cat = _itemsRepository.GetCategory(id);
            if (cat != null)
            {
                CategoryViewModel categoryModel = new CategoryViewModel();
                AutoMapper.Mapper.Map(cat, categoryModel);
                return View(categoryModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //
        // GET: /Management/Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category newCat = new Category() { Name = model.Name };
                _itemsRepository.AddCategory(newCat);
                _itemsRepository.Save();
                return RedirectToAction("Index");
            }

            //return for the user to fix the errors if we got to here
            return View();
        }

        //
        // GET: /Management/Category/Edit/5

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cat = _itemsRepository.GetCategory(id);
            if (cat != null)
            {
                CategoryViewModel catModel = new CategoryViewModel()
                {
                    Name = cat.Name
                };
                return View(catModel);
            }

            return HttpNotFound();
        }

        //
        // POST: /Management/Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category editedCat = new Category() { Id = model.Id, Name = model.Name };
                _itemsRepository.UpdateCategory(editedCat);
                _itemsRepository.Save();
                return RedirectToAction("Index");
            }

            //return to the user to fix the errors
            return View(model);
        }

        //
        // GET: /Management/Category/Delete/5

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category cat = _itemsRepository.GetCategory(id);
            if (cat != null)
            {
                if (cat.CanBeDeleted == false)
                {
                    throw new InvalidOperationException("You can't delete this category!");
                }
                CategoryViewModel catModel = new CategoryViewModel();
                AutoMapper.Mapper.Map(cat, catModel);
                return View(catModel);
            }
            
            return HttpNotFound();
        }

        //
        // POST: /Management/Category/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategoryViewModel catModel)
        {
            _itemsRepository.DeleteCategory(catModel.Id);
            _itemsRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
