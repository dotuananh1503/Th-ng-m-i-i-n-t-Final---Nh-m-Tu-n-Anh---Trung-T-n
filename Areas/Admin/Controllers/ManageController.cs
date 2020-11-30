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
    //[Route("Admin/[controller]/[action]")]
    public class ManageController : Controller
    {
        DataContext dataContext;
        public ManageController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Product> products = dataContext.Products.Select(p => p).ToList();
            List<Category> categories = dataContext.Categories.Select(p => p).ToList();
            List<ContactModel> contacts = dataContext.Contacts.Select(p => p).ToList();
            ViewBag.ProductCount = products.Count();
            ViewBag.CategoryCount = categories.Count();
            ViewBag.ContactCount = contacts.Count();
            return View();
        }
    }
}