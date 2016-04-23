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
    public class RepositoryOrder : Repository<Order>
    {
        public RepositoryOrder(EshopContext context) : base(context)
        {
        }

        public List<Order> GetAllWithItemsAndPaging(int skip, int take)
        {
            return new BuilderOrder(DbSet).IncludeItems().AddPagging(skip, take).ToList();
        } 

        public Order GetWithDetail(int idOrder)
        {
            var builder = new BuilderOrder(DbSet).IncludeAddresses().IncludeUser().IncludeItems().IncludeHistory().IncludeDeliveryPayment().WhereIdOrder(idOrder);

            return builder.Query.FirstOrDefault();
        }

        public List<Order> GetAllWithDetail()
        {
            var builder = new BuilderOrder(DbSet).IncludeAddresses().IncludeItems().IncludeUser();

            return builder.Query.ToList();
        }

        public int GetCountByState(Order.ORDER_STATE state)
        {
            return DbSet.Count(o => o.OrderState == state);
        }

    }
}
