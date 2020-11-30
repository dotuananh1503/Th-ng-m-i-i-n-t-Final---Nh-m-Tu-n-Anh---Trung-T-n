using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huflix.Areas.Admin.Data;
using Huflix.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huflix.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        DataContext dataContext;
        public ContactController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<ContactModel> contacts = dataContext.Contacts.ToList();
            return View(contacts);
        }
        public IActionResult Delete(int id)
        {
            ContactModel contact = dataContext.Contacts.FirstOrDefault(p => p.ContactID == id);

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            ContactModel contact = dataContext.Contacts.FirstOrDefault(p => p.ContactID == id);
            dataContext.Contacts.Remove(contact);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }
    }
}