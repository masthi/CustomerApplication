using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
            
        }
        public ActionResult GotoHome()
        {
            //ViewData["MyTime"] = DateTime.Now.ToString();
            ViewBag.MyTime = DateTime.Now.ToString();
            return View();
        }
    }
}