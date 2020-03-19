using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KidShop.Data;
using KidShop.Models;
using PagedList;

namespace KidShop.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Products
        public ActionResult Index(string searchTerm, string CategoryId, int? page)
        {
            var products = db.Products.Include(p => p.Category).OrderBy(x => x.ProductId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");

            if (String.IsNullOrEmpty(CategoryId))
            {
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    products = db.Products.Where(p => p.ProductName.Contains(searchTerm)).OrderBy(x => x.ProductId);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(CategoryId))
                {
                    if (String.IsNullOrEmpty(searchTerm))
                    {
                        products = db.Products.Where(p => p.CategoryId.Contains(CategoryId)).OrderBy(x => x.ProductId);
                    }
                    else
                    {
                        products = db.Products.Where(p => (p.ProductName.Contains(searchTerm) && p.CategoryId.Contains(CategoryId))).OrderBy(x => x.ProductId);
                    }
                }
            }

            ViewBag.SearchTerm = searchTerm;

            if (page == null) page = 1;
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult ListProduct()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Description,Price,Thumbnail,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ListProduct", "Products", new { });
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Description,Price,Thumbnail,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListProduct", "Products", new { });
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ListProduct", "Products", new { });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
