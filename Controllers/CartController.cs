using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huflix.Areas.Admin.Data;
using Huflix.Areas.Admin.Models;
using Huflix.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Huflix.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext dataContext;
        private readonly string _clientId;
        private readonly string _secretKey;
        public CartController(DataContext dataContext, IConfiguration config)
        {
            this.dataContext = dataContext;
            _clientId = config["PayPalSettings:ClientId"];
            _secretKey = config["PayPalSettings:SecretKey"];
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            else
            {
                ViewBag.cart = null;
                ViewBag.total = 0;
            }

            return View();
        }
        
        public IActionResult Checkout()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            if (cart == null)
                return View();
            else
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            Receipt receipt = new Receipt();
            return View(receipt);
        }
        [HttpPost]
        public IActionResult Checkout(Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                List<DetailReceipt> cart = SessionHelper.GetObjectFromJson<List<DetailReceipt>>(HttpContext.Session, "cart");
                Receipt orderTemp = new Receipt
                {
                    OrderDate = DateTime.Now,
                    Phone = receipt.Phone,
                    Address = receipt.Address,
                    CustomerName = receipt.CustomerName
                };
                dataContext.Receipts.Add(orderTemp);
                dataContext.SaveChanges();
                var query = dataContext.Receipts.FirstOrDefault(p => p.OrderId == orderTemp.OrderId);
                foreach (var item in cart)
                {
                    dataContext.DetailReceipts.Add(new DetailReceipt()
                    {
                        ReceiptId = query.OrderId,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                    });
                }
                dataContext.SaveChanges();
                cart.Clear();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("Success", "Cart");
            }
            return View(receipt);
        }
        public IActionResult PayPalCheckout()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart") == null)
            {
                List<ProductToCart> cart = new List<ProductToCart>();
                cart.Add(new ProductToCart { Product = dataContext.Products.FirstOrDefault(p => p.Id == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new ProductToCart { Product = dataContext.Products.FirstOrDefault(p => p.Id == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<ProductToCart> cart = SessionHelper.GetObjectFromJson<List<ProductToCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}