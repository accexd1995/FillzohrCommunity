using FillzohrCommunity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FillzohrCommunity.Controllers
{
    public class LoginController : Controller
    {
        private FillzohrDbEntities db = new FillzohrDbEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize([Bind(Include = "Username,Password")] User user)
        {
            using (FillzohrDbEntities db = new FillzohrDbEntities())
            {
                User userDetails = db.User.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    user.LoginErrorMessage = "Wrong username or password";
                    return View("Login", user);
                }
                else
                {
                    Session["userId"] = userDetails.UserId;
                    Session["userName"] = userDetails.Username;
                    return View("~/Views/Home/Index.cshtml");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}