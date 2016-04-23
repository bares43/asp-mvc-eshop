using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;
using DataAccess.Repository.Builder;

namespace DataAccess.Repository
{
    public class RepositoryFile : Repository<File>
    {
        public RepositoryFile(EshopContext context) : base(context)
        {
        }

        public List<File> GetByRepository(int idRepository, bool? visible = null)
        {
            var builder = new BuilderFile(DbSet);

            builder.InStorage(idRepository);

            if (visible.HasValue)
            {
                builder.IsVisible(visible.Value);
            }

            return builder.ToList();
        } 
    }
}
