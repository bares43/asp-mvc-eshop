using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class RepositoryPage : Repository<Page>
    {

        public RepositoryPage(EshopContext context) : base(context)
        {
        }

        public Page GetByIdentificator(string url)
        {
            return DbSet.Where(p => p.Identificator.Equals(url)).ToList().First<Page>();
        }

    }
}
