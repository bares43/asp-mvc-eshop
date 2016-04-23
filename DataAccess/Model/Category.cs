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
    public class Category : IEntity
    {
        public int Id { get; set; }

        public virtual Category Parent { get; set; }

        [DisplayName("Název")]
        public string Name { get; set; }

        [DisplayName("Popis")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        
        [DisplayName("Podkategorie")]
        public virtual List<Category> Subcategories { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}
