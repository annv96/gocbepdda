using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GocBepDDA.Models;

namespace GocBepDDA.Controllers
{
    public class CartController : Controller
    {
        DDAEntities db = new DDAEntities();

        // Get Cart
        public List<Cart> GetCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }

        // Add Cart 
        public ActionResult AddCart(int prdId, string strUrl)
        {
            Food food = db.Foods.SingleOrDefault(n => n.Id == prdId);
            if (food == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = GetCart();
            Cart currPrd = listCart.Find(n => n.prdId == prdId);
            if ( currPrd == null)
            {
                currPrd = new Cart(prdId);
                // Add product to cart
                listCart.Add(currPrd);
                return Redirect(strUrl);
            }
            else
            {
                currPrd.prdQuantity++;
                return Redirect(strUrl);
            }
        }

        // Update Cart 
        public ActionResult UpdateCart(int prdId, FormCollection form )
        {
            //Check product
            Food food = db.Foods.SingleOrDefault(n => n.Id == prdId);
            if (food == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = GetCart();
            Cart prd = listCart.SingleOrDefault(n => n.prdId == prdId);
            if (prd != null)
            {
                prd.prdQuantity = int.Parse(form["txtQuantity"].ToString());
            }
            return RedirectToAction("Cart");
        }

        //Delete Cart 
        public ActionResult DeleteCart(int prdId)
        {
            //Check product
            Food food = db.Foods.SingleOrDefault(n => n.Id == prdId);
            if (food == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listCart = GetCart();
            Cart prd = listCart.SingleOrDefault(n => n.prdId == prdId);
            if (prd != null)
            {
                listCart.RemoveAll(n => n.prdId == prdId);            
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }

        // Calculate TotalPayment 
        private double TotalPayment()
        {
            double cartTotalPayment = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                cartTotalPayment = listCart.Sum(n => n.prdTotal);
            }
            return cartTotalPayment;
        }

        // Calculate TotalAmount
        private double TotalAmount()
        {
            int cartTotalAmount = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                cartTotalAmount = listCart.Sum(n => n.prdQuantity);
            }
            return cartTotalAmount;
        }

        // Show Cart
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> listCart = GetCart();
            return View(listCart);
        }

        // Partial Cart 
        public ActionResult CartPartial()
        {
            if (TotalAmount() == 0)
            {
                return PartialView();
            }
            ViewBag.TotalAmount = TotalAmount();
            ViewBag.TotalPayment = TotalPayment();
            return PartialView();
        }
    }
}