using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository.Builder
{
    class BuilderOrder : Builder<Order>
    {
        public BuilderOrder(DbSet<Order> DbSet) : base(DbSet)
        {
        }

        public BuilderOrder IncludeItems()
        {
            query = Query.Include(o => o.OrderItems).Include(o => o.OrderItems.Select(i => i.Product));
            return this;
        }

        public BuilderOrder IncludeAddresses()
        {
            query = Query.Include(o => o.DeliveryAddress).Include(o => o.BillingAddress);
            return this;
        }

        public BuilderOrder IncludeUser()
        {
            query = Query.Include(o => o.Customer);
            return this;
        }

        public BuilderOrder IncludeHistory()
        {
            query = Query.Include(o => o.OrderHistories).Include(o => o.OrderHistories.Select(i => i.ChangedBy));
            return this;
        }

        public BuilderOrder IncludeDeliveryPayment()
        {
            query = Query.Include(o => o.Delivery).Include(o => o.Payment);
            return this;
        }

        public BuilderOrder WhereIdOrder(int idOrder)
        {
            query = Query.Where(o => o.Id == idOrder);
            return this;
        }
    }
}
