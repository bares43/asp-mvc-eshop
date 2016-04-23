using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Models.View
{
    public class ProductPreview
    {
        public Product Product { get; set; }

        public Image Image { get; set; }
    }
}