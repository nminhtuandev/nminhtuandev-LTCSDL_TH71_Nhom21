using ProductMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMvc.Controllers
{
    public class ProductController : Controller
    {
        VinaEntity db = new VinaEntity();
        #region Show Product
        public ActionResult Index()
        {
            return View(db.Categorys);
        }
        public ActionResult ProductId(int id)
        {
            List<Product> book = db.Products.Where(n=>n.CategoryID==id).ToList();
            if (book.Count == 0)
            {
                ViewBag.book = "Không có sách chủ đề này";
            }
            return View(book);
        }
        public ActionResult Details(int id)
        {
            return View(db.Products.Find(id));
        }
        public ActionResult Top5Product()
        {
            return PartialView(db.Products.Take(5));
        }
        #endregion
        #region CRUD
         #region Create
        [HttpGet, Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categorys, "CategoryID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ProductID")]Product model)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.ProductID });
            }
            ViewBag.CategoryID = new SelectList(db.Categorys, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }
        #endregion

        #region Edit
        
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            ViewBag.CategoryID = new SelectList(db.Categorys, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.ProductID });
            }
            ViewBag.CategoryID = new SelectList(db.Categorys, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }
        #endregion
        #region Delete .Dùng Ajax
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return new EmptyResult();
        }
        #endregion
        #endregion

    }
}
