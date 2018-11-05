using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GocBepDDA.Models;

namespace GocBepDDA.Controllers
{
    public class FoodDetailController : Controller
    {

        DDAEntities db = new DDAEntities();

        // GET: FoodDetail
        public ViewResult Detail(int? Id)
        {
            Food food = db.Foods.SingleOrDefault(n=>n.Id == Id);
            if (food == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(food);
        }

    }
}