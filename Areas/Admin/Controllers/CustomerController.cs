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
    public class CustomerController : Controller
    {
        DataContext dataContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CustomerController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dataContext = dataContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Customer> customers = dataContext.Customers.ToList();
            return View(customers);
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
