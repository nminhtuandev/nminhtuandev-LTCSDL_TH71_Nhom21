using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductMvc.Models
{
    public class VinaLogic
    {
        #region Lấy về User đăng nhập
        public static User UserLogOn()
        {
            VinaEntity db = new VinaEntity();
            return db.Users.FirstOrDefault(m => m.UserName == System.Web.HttpContext.Current.User.Identity.Name);
        }
        #endregion
        #region Check UserLogOn
        public static User CheckUser(string UserName, string Password)
        {
            var passMD5 = Password.GetMD5();
            VinaEntity db = new VinaEntity();
            var UserLogOn = db.Users.SingleOrDefault(m => m.UserName == UserName && m.Password == passMD5);
            return UserLogOn;
        }
        #endregion
    }
}