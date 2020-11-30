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
    public class ReceiptController : Controller
    {
        DataContext dataContext;
        public ReceiptController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Receipt> receipts = dataContext.Receipts.ToList();
            return View(receipts);
        }
      
        public IActionResult Delete(int id)
        {
            Receipt receipt = dataContext.Receipts.FirstOrDefault(p => p.OrderId == id);

            return View(receipt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Receipt receipt = dataContext.Receipts.FirstOrDefault(p => p.OrderId == id);
            dataContext.Receipts.Remove(receipt);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Receipt");
        }

    }
}