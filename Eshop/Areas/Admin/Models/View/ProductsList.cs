using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;
using Eshop.Class;

namespace Eshop.Areas.Admin.Models.View
{
    public class ProductsList
    {
        public List<Product> Products { get; set; } 

        public Paginator Paginator { get; set; }
    }
}