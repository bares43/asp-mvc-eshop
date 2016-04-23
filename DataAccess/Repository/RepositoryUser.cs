using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using System.Data.Entity;
using DataAccess.Class;
using DataAccess.Repository.Builder;

namespace DataAccess.Repository
{
    public class RepositoryUser : Repository<User>
    {
        public RepositoryUser(EshopContext context) : base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            return DbSet.Include(u => u.Roles).FirstOrDefault(u => u.Email.Equals(email));
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return DbSet.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
        }

     /*   public List<Administrator> GetAllAdministrators()
        {
            return new BuilderUser(DbSet).GetAdministrators().ToList();
        } */
        
    }
}
