using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class File : IEntity
    {
        public static string Path => HttpContext.Current.Server.MapPath("~/Uploads/files/");

        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string Description { get; set; }

        public bool Visible { get; set; }

        public int? FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
    }
}
