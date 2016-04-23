using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class RepositoryAddress : Repository<Address>
    {
        public RepositoryAddress(EshopContext context) : base(context)
        {
        }

        public List<Address> GetAddressesByUserAndType(User user, Address.AddressTypes type)
        {
            return DbSet.Where(a => a.User.Id == user.Id && a.Type == type).OrderByDescending(a => a.Primary).ThenByDescending(a => a.Created).ToList();
        } 
    }
}
