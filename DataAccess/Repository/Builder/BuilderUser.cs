using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository.Builder
{
    class BuilderUser : Builder<User>
    {
        public BuilderUser(DbSet<User> DbSet) : base(DbSet)
        {
        }

        public BuilderUser GetAdministrators()
        {
            query = Query.OfType<Administrator>();
            return this;
        }

        public BuilderUser GetCustomers()
        {
            query = Query.OfType<Customer>();
            return this;
        }
    }
}
