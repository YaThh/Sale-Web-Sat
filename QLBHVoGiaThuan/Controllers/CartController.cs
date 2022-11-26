using QLBHVoGiaThuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHVoGiaThuan.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        public List<Cart> GetListCarts()
        {
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }
        public ActionResult AddCart(int id)
        {
            List<Cart> carts = GetListCarts();
            Cart c = carts.Find(s => s.ProductID == id);
            if (c == null)
            {
                c = new Cart(id);
                carts.Add(c);
            }
            else
            {
                c.Quantity++;
            }
            return RedirectToAction("ListCarts");
        }

        public ActionResult ListCarts()
        {
            List<Cart> carts = GetListCarts();
            ViewBag.Count = carts.Sum(s => s.Quantity);
            ViewBag.Total = carts.Sum(s => s.Total);
            return View(carts);
        }

        public ActionResult Delete(int id)
        {
            List<Cart> carts = GetListCarts();
            Cart c = carts.Find(s => s.ProductID == id);
            if (c != null)
            {
                carts.RemoveAll(s => s == c);
            }
            return RedirectToAction("ListCarts");
        }
    }
}