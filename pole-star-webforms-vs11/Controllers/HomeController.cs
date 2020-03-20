using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoleStarWebVS11.Controllers
{
    [HandleError]
    [Localization]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = Session["userinfo"];

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}
