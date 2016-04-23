using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class RepositoryDeliveryPayment : Repository<DeliveryPayment>
    {
        public RepositoryDeliveryPayment(EshopContext context) : base(context)
        {
        }
    }
}
