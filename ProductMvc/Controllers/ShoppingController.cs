using ProductMvc.Models;
using ProductMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMvc.Controllers
{
    public class ShoppingController : Controller
    {
        // Hiển thị danh sách đơn hàng
        VinaEntity db = new VinaEntity();
        public ActionResult Index()
        {
            return View(db.Orders);
        }
        #region Create Order
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "OrderID")]Order model)
        {
            var CartList = (List<CartVM>)Session["Cart"];
            var OrdetailsList = new List<OrderDetail>();
            CartList.ForEach(m =>
            {
                var _Product = db.Products.Find(m.Product.ProductID);
                OrdetailsList.Add(new OrderDetail
                {
                    ProductID = _Product.ProductID,
                    Quantity = m.Quantity
                });
            });
            model.Active = false;
            // thêm danh sách OrderDetails
            model.OrderDetails = OrdetailsList;
            // Thêm bảng Order
            db.Orders.Add(model);
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("CreateSuccess");
        }
        public ActionResult CreateSuccess()
        {
            return View();
        }
        #endregion
        #region Details Order
        public ActionResult Details(int id)
        {
            return View(db.Orders.Find(id));
        }
        #endregion
        #region Delete Order
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var _Order = db.Orders.Find(id);
            _Order.OrderDetails.ToList().ForEach(m => db.OrderDetails.Remove(m));
            db.Orders.Remove(_Order);
            db.SaveChanges();
            return new EmptyResult();
        }
        #endregion

        #region ShoppingCart
        public ActionResult ShowCart()
        {
            return View();
        }
        
        public ActionResult AddToCart(int id)
        {
            //string SK = fc["txtSL"].ToString();
            var _Product = db.Products.Find(id);
            var CartList = new List<CartVM>();
            if (Session["Cart"] != null)
            {
                CartList = (List<CartVM>)Session["Cart"];
                var OldCart = CartList.Find(m => m.Product.ProductID == id);//OldCart.Quantity + 1
                if (OldCart != null)
                {
                    var NewCart = new CartVM { Product = _Product,Quantity=1 };
                    CartList.Remove(OldCart);
                    CartList.Add(NewCart);
                }
                else
                {
                    CartList.Add(new CartVM { Product = _Product, Quantity = 1 });
                    
                }
            }
            else
            {
               CartList.Add(new CartVM { Product = _Product, Quantity =1});
                
            }
            Session["Cart"] = CartList;
            return RedirectToAction("ShowCart");
        }
        public ActionResult RemoveCart(int id)
        {
            var CartList = (List<CartVM>)Session["Cart"];
            var _Cart = CartList.SingleOrDefault(m => m.Product.ProductID == id);
            CartList.Remove(_Cart);
            Session["Cart"] = CartList;
            return new EmptyResult();
        }
        #endregion
        public RedirectToRouteResult fixQuantity(int MaSP, int soluongmoi)
        {
            List<CartVM> Cart = Session["Cart"] as List<CartVM>;
            CartVM itemFix = Cart.FirstOrDefault(m => m.ProductId == MaSP);
            if (itemFix != null)
            {
                itemFix.Quantity = soluongmoi;
            }
            return RedirectToAction("ShowCart");
        }
    }
}
