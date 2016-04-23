using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Eshop.Class;

namespace Eshop.Class
{
    public class SessionCart : ICart
    {
        public List<CartItem> Items {get; set; }

        public SessionCart()
        {
            if (HttpContext.Current.Session["cart"] == null)
            {
                HttpContext.Current.Session["cart"] = new List<CartItem>();
            }
            Items = (List<CartItem>)HttpContext.Current.Session["cart"];
        }
        
        public double GetTotalPrice()
        {
            double price = 0;
            Items.ForEach(item =>
            {
                price += item.Amount*item.Product.Price;
            });
            return price;
        }

        public int GetAmount()
        {
            int amount = 0;
            Items.ForEach(item =>
            {
                amount += item.Amount;
            });
            return amount;
        }

        public int GetAmountUnique()
        {
            return Items.Count;
        }
    }
}