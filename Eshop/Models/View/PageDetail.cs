using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Models.View
{
    public class PageDetail
    {
        public Page Page { get; set; }

        public List<Image> Images { get; set; }
        
        public List<File> Files { get; set; } 
    }
}