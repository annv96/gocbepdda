using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GocBepDDA.Models;

namespace GocBepDDA.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        DDAEntities db = new DDAEntities();

        public ActionResult Index()
        {
            return View(db.Foods.ToList());
        }

        public PartialViewResult FoodMenuPartial()
        {
            return PartialView(db.Foods.ToList());
        }

        public PartialViewResult UserPartial()
        {
            if (Session["UserLogin"] != null)
            {
                User user = (User)Session["UserLogin"];
                ViewBag.Name = user.Name;
                return PartialView();
            }
            return PartialView();
        }

        public ActionResult Logout()
        {
            Session.Remove("UserLogin");
            return RedirectToAction("Index","Home");
        }
    }
}