using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCookie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Response.Cookies["id"].Value = "SODADM";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var x = Request.Cookies["id"].Value;
            //Response.Cookies
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}