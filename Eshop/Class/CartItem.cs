using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Class
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}