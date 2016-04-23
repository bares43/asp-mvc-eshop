using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Models.View
{
    public class ProductDetail
    {
        public Product Product { get; set; }

        public List<Image> Images { get; set; }

        public Image MainImage
        {
            get
            {
                if (Images != null && Images.Any())
                {
                    return Images.FirstOrDefault();
                }
                return null;
            }
        }

        public List<Product> RelatedProducts { get; set; } 

        public List<File> Files { get; set; } 
    }
}