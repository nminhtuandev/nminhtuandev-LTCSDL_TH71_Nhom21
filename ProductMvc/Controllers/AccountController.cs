using ProductMvc.Models;
using ProductMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace ProductMvc.Controllers
{
    public class AccountController : Controller
    {

        VinaEntity db = new VinaEntity();
        public ActionResult Index()
        {
            return View(db.Users);
        }
        #region LogOn
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                var UserLogOn = VinaLogic.CheckUser(model.UserName, model.Password);
                if (UserLogOn != null)
                {
                    FormsAuthentication.SetAuthCookie(UserLogOn.UserName, true);
                    return RedirectToAction("Index");
                }
                return View("LogOn", model);
            }
            return View("LogOn", model);
        }
        #endregion
        #region LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Product");
        }
        #endregion
        #region Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var _OldUserName = db.Users.FirstOrDefault(m => m.UserName == model.UserName);
                if (_OldUserName == null)
                {
                    var _User = new User { UserName = model.UserName, Password = model.Password.GetMD5() };
                    db.Users.Add(_User);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Tên đăng nhập đã tồn tại";
            return View(model);
        }
        #endregion

        #region Đổi mật khẩu
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var UserLogOn = VinaLogic.UserLogOn();
                if (UserLogOn.Password == model.OldPassword.GetMD5())
                {
                    UserLogOn.Password = model.NewPassword;
                    db.SaveChanges();
                    ViewBag.Message = "Đổi mật khẩu thành công";
                }
                else
                {
                    ViewBag.Message = "Mật Khẩu cũ không đúng";
                }
                return View();
            }
            return View();
        }
        #endregion

    }
}
