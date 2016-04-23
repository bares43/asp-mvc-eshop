using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }

        public virtual Order Order { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int _amount;
        public int Amount {
            get { return _amount; }
            set
            {
                if (value > 0)
                {
                    _amount = Math.Min(value, MAX_AMOUNT_OF_PRODUCT);
                }
            }
        }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        public static int MAX_AMOUNT_OF_PRODUCT = 99;
    }
}
