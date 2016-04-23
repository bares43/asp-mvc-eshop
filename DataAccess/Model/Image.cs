using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Image : IEntity
    {
        public static string Path => HttpContext.Current.Server.MapPath("~/Uploads/images/");


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Visible { get; set; }

        public int? ParentId { get; set; }
        public Image Parent { get; set; }

        public List<Image> Thumbnails { get; set; } 

        public int Width { get; set; }

        public int Height { get; set; }

        public int RealWidth { get; set; }

        public int RealHeight { get; set; }

        public string ContentType { get; set; }

        public int GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }

        public Image GetThumbnail(int width, int height)
        {
            return Thumbnails.FirstOrDefault(t => t.Width == width && t.Height == height);
        }
        
    }
}
