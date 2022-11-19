using QLBHVoGiaThuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHVoGiaThuan.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        NorthwindDataContext da = new NorthwindDataContext();
        public ActionResult ListProduct()
        {
            List<Product> ds = da.Products.Select(s => s).ToList();
            return View(ds);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product p, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (da.Products.FirstOrDefault(s => s.ProductName == p.ProductName) == null)
                {
                    p.SupplierID = int.Parse(collection["NCC"]);
                    p.CategoryID = int.Parse(collection["LSP"]);
                    da.Products.InsertOnSubmit(p);
                    da.SubmitChanges();

                    return RedirectToAction("ListProduct");
                }
                else
                {
                    return RedirectToAction("Create");
                }

               
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
