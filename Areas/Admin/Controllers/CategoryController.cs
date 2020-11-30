using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huflix.Areas.Admin.Data;
using Huflix.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huflix.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        DataContext dataContext;
        public CategoryController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Category> categories = dataContext.Categories.ToList();
            return View(categories);
        }
        [HttpGet]

        public IActionResult Add()
        {
            Category category = new Category();
            return View(category);
        }
        [HttpPost]

        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category();

                newCategory.CategoryId = category.CategoryId;
                newCategory.CategoryName = category.CategoryName;


                dataContext.Categories.Add(newCategory);
                dataContext.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View(category);
            }
        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            Category categories = dataContext.Categories.FirstOrDefault(p => p.CategoryId == id);


            return View(categories);
        }
        [HttpPost]

        public IActionResult Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {


                Category p = dataContext.Categories.FirstOrDefault(p => p.CategoryId == id);
                p.CategoryName = category.CategoryName;

                dataContext.SaveChanges();

                ViewBag.Status = 1;
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = dataContext.Categories.FirstOrDefault(p => p.CategoryId == id);

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Category category = dataContext.Categories.FirstOrDefault(p => p.CategoryId == id);
            dataContext.Categories.Remove(category);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}