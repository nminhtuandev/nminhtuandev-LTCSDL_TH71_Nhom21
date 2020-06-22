using ProductMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMvc.Controllers
{
    public class CategoryController : Controller
    {
        VinaEntity db = new VinaEntity();
        public ActionResult Index()
        {
            return View(db.Categorys);
        }
        #region Create
        [HttpGet]
        public ActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "CategoryID")]Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return View(model);
        }
        #endregion
        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {           
            return View(db.Categorys.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }           
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var _Category = db.Categorys.Find(id);           
            _Category.Products.ToList().ForEach(m=>db.Products.Remove(m));
            db.Categorys.Remove(_Category);
            db.SaveChanges();
            return new EmptyResult();
        }
        #endregion
    }
}
