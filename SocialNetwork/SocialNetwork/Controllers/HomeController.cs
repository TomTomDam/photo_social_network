using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The application's tech stack and application features.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Let's get in touch!";

            return View();
        }
    }
}