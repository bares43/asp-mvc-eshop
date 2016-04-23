using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class RepositoryGallery : Repository<Gallery>
    {
        public RepositoryGallery(EshopContext context) : base(context)
        {
        }
    }
}
