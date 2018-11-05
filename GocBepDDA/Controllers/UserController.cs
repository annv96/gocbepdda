using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GocBepDDA.Models;

namespace GocBepDDA.Controllers
{
    public class UserController : Controller
    {
        DDAEntities db = new DDAEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                //Insert User
                db.Users.Add(user);
                db.SaveChanges();

            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection login)
        {
            string userName = login["Username"].ToString();
            string userPsw = login["Psw"].ToString();
            User user = db.Users.SingleOrDefault(n => n.Username == userName && n.Password == userPsw);
            if (user != null)
            {
                Session["UserLogin"] = user;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}