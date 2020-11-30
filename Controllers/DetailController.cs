using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huflix.Areas.Admin.Data;
using Huflix.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huflix.Controllers
{
    public class DetailController : Controller
    {
        private readonly DataContext dataContext;
        public DetailController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet("Detail/{id}/{name}")]
        public IActionResult Index(int id)
        {
            Product product =
                dataContext.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}