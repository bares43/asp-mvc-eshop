using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class OrderHistory : IEntity
    {
        public enum OrderHistoryTypes { OrderStateChange, OrderEdit}

        public int Id { get; set; }

        public Order Order { get; set; }

        public OrderHistoryTypes Type { get; set; }

        public string Message { get; set; }

        public Order.ORDER_STATE OrderState { get; set; }

        public DateTime Date { get; set; }

        public Administrator ChangedBy { get; set; }

    }
}
