using QLBHVoGiaThuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;

namespace QLBHVoGiaThuan.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private NorthwindDataContext da = new NorthwindDataContext();

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

        public ActionResult OrderProduct()
        {
            int maDH;
            using (TransactionScope tran = new TransactionScope())
            {
                try
                {
                    List<Cart> carts = GetListCarts();
                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    da.Orders.InsertOnSubmit(order);
                    da.SubmitChanges();
                    maDH = da.MaxOrder().Single().OrderID;

                    foreach (var item in carts)
                    {
                        Order_Detail d = new Order_Detail();
                        d.OrderID = maDH;
                        d.ProductID = item.ProductID;
                        d.UnitPrice = (decimal)item.UnitPrice;
                        d.Quantity = (short)item.Quantity;
                        d.Discount = 0;
                        da.Order_Details.InsertOnSubmit(d);
                    }
                    da.SubmitChanges();
                    tran.Complete();
                    Session["Cart"] = null;
                    return RedirectToAction("ListOrders", DateTime.Now.Date);
                }
                catch (Exception ex)
                {
                    tran.Dispose();
                    return Content(ex.Message);
                }
            } 
        }

        public ActionResult ListOrders(DateTime? orderDate)
        {
            List<Order> ds = da.Orders.Where(s => s.OrderDate.ToString().Contains(orderDate.ToString()))
                                      .OrderByDescending(s => s.OrderID)
                                      .ToList();
            return View(ds);
        }
    }
}