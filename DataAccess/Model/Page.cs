using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Page : IEntity
    {
        public int Id { get; set; }

        [DisplayName("Identifikátor")]
        public string Identificator { get; set; }

        [DisplayName("Název")]
        public string Name { get; set; }

        [DisplayName("Obsah")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int? GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }

        public int? FileStorageId { get; set; }
        public virtual FileStorage FileStorage { get; set; }
    }
}
