using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Huflix.Models;
using Huflix.Areas.Admin.Models;
using Huflix.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Huflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DataContext dataContext;
        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            List<Product> movies = dataContext.Products.ToList();
            List<Category> categories = dataContext.Categories.ToList();
            ViewBag.Categories = categories;
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //public IActionResult Detail()
        //{
        //    return View();
        //}
        public IActionResult Signup()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Visitor, Administrator")]

        public IActionResult Contact()
        {
            ContactModel contact = new ContactModel ();
            return View(contact);
        }
        [HttpPost]
        [Authorize(Roles = "Visitor, Administrator")]
        public IActionResult Contact(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                ContactModel newContact = new ContactModel();

                newContact.ContactID = contact.ContactID;
                newContact.ContactName = contact.ContactName;
                newContact.ContactPhone = contact.ContactPhone;
                newContact.ContactAddress = contact.ContactAddress;
                newContact.ContactEmail = contact.ContactEmail;
                newContact.ContactNote = contact.ContactNote;

                dataContext.Contacts.Add(newContact);
                dataContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(contact);
            }
        }

        
       
        [HttpGet("Watching/{id}/{name}")]
        [Authorize(Roles = "Visitor, Administrator")]
        public IActionResult Search(string search)
        {
            var query = from m in dataContext.Products.Include(m => m.Category)
                        select m;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Name.Contains(search));
            return View(query.ToList());
        }
        public IActionResult HigherSearch(string search, int categoryID = 0)
        {
            var categories = from c in dataContext.Categories select c;
            ViewBag.categoryID = new SelectList(categories, "CategoryId", "CategoryName");
            var query = from m in dataContext.Products.Include(m => m.Category)
                        select m;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(a => a.Name.Contains(search));
            if (categoryID != 0)
            {
                query = query.Where(x => x.CategoryId == categoryID);
            }
            return View(query.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Visitor, Administrator")]
        public ActionResult Comment(int productIdComment, string nameComment, string detailComment, string timeComment)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = new Comment();
                newComment.ProductId = productIdComment;
                newComment.CommentName = nameComment;
                newComment.CommentDescription = detailComment;
                newComment.CommentTime = timeComment;

                dataContext.Comments.Add(newComment);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
