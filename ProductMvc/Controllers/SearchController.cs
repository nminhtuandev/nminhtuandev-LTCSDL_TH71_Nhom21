using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductMvc.Models;

namespace ProductMvc.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        VinaEntity db = new VinaEntity();
        [HttpPost]
        public ActionResult ResultSearch(FormCollection f)
        {
            string sKey = f["txtSearch"].ToString();
            List<Product> lstSearch = db.Products.Where(n => n.ProductName.Contains(sKey)).ToList();
            return View(lstSearch);
        }

    }
}
