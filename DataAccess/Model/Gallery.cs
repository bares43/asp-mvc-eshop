using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Gallery : IEntity
    {
        public int Id { get; set; }

        public virtual List<Image> Images { get; set; } 

        public bool Visible { get; set; }
    }
}
