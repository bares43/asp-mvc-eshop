using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;
using Eshop.Class;

namespace Eshop.Models.View
{
    public class ProductList
    {
        public List<Product> Products { get; set; }
        
        public Category Category { get; set; }
        
        public string Phrase { get; set; }
        
        public Paginator Paginator { get; set; } 
    }
}