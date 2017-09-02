using CustomerApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Authenticate()
        {
            return View("Authenticate");
        }
        public ActionResult Validate()
        {
            //Forms Authentication
            User obj = new User();
            obj.UserName = Request.Form["UserName"];
            obj.Password = Request.Form["Password"];

            DataAccessLayer dal = new DataAccessLayer();
            List<User> users = (from u in dal.Users.ToList<User>()
                                where u.UserName == obj.UserName && u.Password == obj.Password
                                select u).ToList<User>();
            if (users.Count ==1)
            {
                FormsAuthentication.SetAuthCookie("Cookie", true);
                return View("../Home/GotoHome");
            }
            else
            {
                return View("Authenticate");

            }

        }
    }
}