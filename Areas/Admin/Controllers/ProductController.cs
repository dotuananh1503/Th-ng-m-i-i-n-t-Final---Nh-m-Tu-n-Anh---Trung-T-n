using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Huflix.Areas.Admin.Data;
using Huflix.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Huflix.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    //[Route("Admin/[controller]/[action]")]
    public class ProductController : Controller
    {
        DataContext dataContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dataContext = dataContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = dataContext.Products.ToList();
            List<Category> categories = dataContext.Categories.ToList();
            List<SelectListItem> cate = new List<SelectListItem>();
            foreach (var item in categories)
            {
                cate.Add(new SelectListItem()
                {
                    Value = item.CategoryId.ToString(),
                    Text = item.CategoryName
                });
            }
            ViewBag.Categories = cate;
            return View(products);
        }

        [HttpGet]

        public IActionResult Add()
        {
            Product product = new Product();
            ViewBag.Categories = new SelectList(dataContext.Categories, "CategoryId", "CategoryName");
            return View(product);
        }

        [HttpPost]

        public IActionResult Add(Product product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                if (photo == null || photo.Length == 0)
                {
                    newProduct.Image = "default.jpg";
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    photo.CopyToAsync(stream);
                    newProduct.Image = photo.FileName;
                }
                newProduct.Id = product.Id;
                newProduct.Code = product.Code;
                newProduct.Name = product.Name;
                newProduct.CategoryId = product.CategoryId;
                newProduct.Price = product.Price;
                newProduct.Description = product.Description;

                dataContext.Products.Add(newProduct);
                Category categoryModel = dataContext.Categories.FirstOrDefault(p => p.CategoryId == product.CategoryId);
                categoryModel.CategoryCountProduct++;

                dataContext.SaveChanges();

                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = dataContext.Products.FirstOrDefault(p => p.Id == id);
            List<Category> categories = dataContext.Categories.ToList();
            List<SelectListItem> cate = new List<SelectListItem>();
           
            foreach (var item in categories)
            {
                cate.Add(new SelectListItem()
                {
                    Value = item.CategoryId.ToString(),
                    Text = item.CategoryName
                });
            }
            ViewBag.Categories = cate;
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                Product p = dataContext.Products.FirstOrDefault(p => p.Id == id);
               
                p.Code = product.Code;
                p.Name = product.Name;
                p.CategoryId = product.CategoryId;
                p.Price = product.Price;
                p.Description = product.Description;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            Product product = dataContext.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await dataContext.Products.FindAsync(id);
            var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images/products", product.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}