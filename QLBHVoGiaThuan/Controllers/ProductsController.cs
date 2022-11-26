using QLBHVoGiaThuan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            return View(p);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            
                // TODO: Add update logic here
                Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
                p.ProductName = collection["ProductName"];
                p.UnitPrice = decimal.Parse(collection["UnitPrice"]);
                p.UnitsInStock = short.Parse(collection["UnitsInStock"]);
                p.CategoryID = int.Parse(collection["LSP"]);
                p.SupplierID = int.Parse(collection["NCC"]);

                UpdateModel(p);
                da.SubmitChanges();

                return RedirectToAction("ListProducts");
        
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {

            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            return View(p);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // TODO: Add update logic here
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            try
            {
                da.Products.DeleteOnSubmit(p);
                da.SubmitChanges();
            }
            catch (SqlException ex)
            {
                ViewBag.EX = ex;
                return RedirectToAction("Delete");
            }

            return RedirectToAction("ListProducts");
        }
    }
}
