using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository.Builder
{
    class BuilderFile : Builder<File>
    {
        public BuilderFile(DbSet<File> DbSet) : base(DbSet)
        {
        }

        public BuilderFile IsVisible(bool isVisible = true)
        {
            query = Query.Where(p => (p.Visible && isVisible) || (!p.Visible && !isVisible));
            return this;
        }

        public BuilderFile InStorage(int idStorage)
        {
            query = Query.Where(i => i.FileStorage.Id == idStorage);
            return this;
        }
    }
}
