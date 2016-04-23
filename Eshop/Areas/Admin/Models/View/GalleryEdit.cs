using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Areas.Admin.Models.View
{
    public class GalleryEdit
    {
        public Gallery Gallery { get; set; }

        public List<Image> Images { get; set; }
    }
}