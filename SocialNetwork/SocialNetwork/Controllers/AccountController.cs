using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        //Account DBContext
        AccountContext db = new AccountContext();

        //GET: Account/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.user.ToList());
        }

        //POST: Account/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Account user)
        {
            var usr = db.user.Single(u => u.username == user.username && u.password == user.password);

            if (usr != null)
            {
                Session["id"] = usr.id.ToString();
                Session["username"] = usr.username.ToString();

                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is not correct.");
            }
            return View();
        }

        //GET: Account/LoggedIn
        [HttpGet]
        public ActionResult LoggedIn()
        {
            if (Session["id"] != null)
            {
                return View("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Account/Logoff

        //GET: Account/Register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Account/Register
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                db.user.Add(account);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = account.username + " " + "has been successfully registered.";
            }

            return View(account);
        }

        //GET: Account/ResetPassword

        //POST: Account/ResetPassword
    }
}