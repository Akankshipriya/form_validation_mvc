using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            using (DB db=new DB())
            {
                db.Users.Add(user);
                db.SaveChanges();
                ModelState.Clear();
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            using(DB db=new DB())
            {
                var user = db.Users.Where(a => a.Email == login.Email && a.Password == login.Password).FirstOrDefault();
                if(user!=null)
                {
                    var Ticket = new FormsAuthenticationTicket(login.Email, true, 3000);
                    string Encrypt = FormsAuthentication.Encrypt(Ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    if(user.RoleId==1)
                    {
                        return RedirectToAction("UserArea", "Home");
                    }
                    else
                    {
                        return RedirectToAction("AdminArea", "Home");
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

    }
}