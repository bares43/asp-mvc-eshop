using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class RepositoryCategory : Repository<Category>
    {
        public RepositoryCategory(EshopContext context) : base(context)
        {
        }

        public List<Category> GetMainCategories()
        {
            return DbSet.Where(c => c.Parent == null).ToList();
        }

    }
}
