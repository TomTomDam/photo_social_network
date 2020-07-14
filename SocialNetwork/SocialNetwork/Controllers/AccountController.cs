using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SocialNetwork.Controllers.ActionFilters;

namespace SocialNetwork.Controllers
{
    [AuthorizationFilter]
    public class AccountController : Controller
    {
        //Account DBContext
        AccountContext db = new AccountContext();

        //GET: Account/Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //POST: Account/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {
            //Map LoginViewModel to Account model
            Account model = new Account()
            {
                id = user.id,
                username = user.username,
                password = user.password,
                rememberMe = user.rememberMe
            };

            var usr = db.user.FirstOrDefault(u => u.username == user.username && u.password == user.password);

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

            TempData["LoginMessage"] = "You must login before you can do that action.";

            //Remember username and password
            if (usr != null && user.rememberMe == true)
            {
                HttpCookie rememberMe = new HttpCookie("Remember me");
                rememberMe.Values.Add("Username", model.username);
                rememberMe.Values.Add("Password", model.password);
                rememberMe.Expires = DateTime.Now.AddDays(7);
                Response.SetCookie(rememberMe);
                Response.Cookies.Add(rememberMe);

                if (Request.Cookies["Remember me"] != null)
                {
                    model.username = Request.Cookies["Remember me"].Values["Username"];
                    model.password = Request.Cookies["Remember me"].Values["Password"];
                }
            }

            return View();
        }

        //Account/LoggedIn
        public ActionResult LoggedIn()
        {
            if (Session["id"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
        
        //GET: Account/Person
        public ActionResult Person(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Account account = db.user.Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

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
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username, password, email, confirmPassword")]RegisterViewModel user)
        {
            //Map RegisterViewModel to Account model
            Account model = new Account()
            {
                id = user.id,
                username = user.username,
                email = user.email,
                password = user.password,
                confirmPassword = user.confirmPassword,
            };

            if (ModelState.IsValid)
            {
                db.user.Add(model);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = user.username + " " + "has been successfully registered. Redirecting you to the home page...";
            }
            else
            {
                ModelState.AddModelError("", "We could not register your account.");
            }

            return View(user);
        }

        //Check if username already exists
        [AllowAnonymous]
        public JsonResult UsernameAlreadyExists(RegisterViewModel user, string username)
        {
            return Json(!db.user.Any(u => u.username == username), JsonRequestBehavior.AllowGet);
        }
        //Check if email already exists
        [AllowAnonymous]
        public JsonResult EmailAlreadyExists(RegisterViewModel user, string email)
        {
            return Json(!db.user.Any(u => u.email == email), JsonRequestBehavior.AllowGet);
        }

        //GET: Account/ResetPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword(ResetPasswordMessage message)
        {
            ViewBag.StatusMessage = 
                message == ResetPasswordMessage.ChangePasswordSuccess ? "Your password has been changed."
                : message == ResetPasswordMessage.SetPasswordSuccess ? "Your password has been set."
                : "";

            ViewBag.ReturnUrl = Url.Action("ResetPassword");
            return View();
        }

        public enum ResetPasswordMessage
        {
            ChangePasswordSuccess,
            SetPasswordSuccess
        }

        //POST: Account/ResetPassword
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int? id, ResetPasswordViewModel user, ResetPasswordMessage message) //ResetPasswordViewModel user
        {
            //Map ResetPasswordViewModel to Account mdoel
            Account model = new Account()
            {
                oldPassword = user.oldPassword,
                newPassword = user.newPassword,
                confirmResetPassword = user.confirmResetPassword
            };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resetPassword = db.user.Find(id);

            if (TryUpdateModel(resetPassword, "", new string[] { "password" }))
            {
                db.SaveChanges();

                return RedirectToAction("ResetPassword", new { message = ResetPasswordMessage.ChangePasswordSuccess });
            }
            else
            {
                ModelState.AddModelError("", "We could not reset your password.");
            }

            ViewBag.ReturnUrl = Url.Action("ResetPassword");
            return View(resetPassword);
        }
    }
}