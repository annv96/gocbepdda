using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GocBepDDA.Models
{
    public class Cart
    {
        DDAEntities db = new DDAEntities();
        public int prdId { get; set; }
        public string prdName { get; set; }
        public string prdImage { get; set; }
        public double prdPrice { get; set; }
        public int prdQuantity { get; set; }
        public double prdTotal { get {
                return prdPrice * prdQuantity;
            }
        }
        // Create Cart 
        public Cart(int Id)
        {
            prdId = Id;
            Food food = db.Foods.Single(n => n.Id == prdId);
            prdName = food.Name;
            prdImage = food.Image;
            prdPrice = food.Price;
            prdQuantity = 1;
        }


    }
}