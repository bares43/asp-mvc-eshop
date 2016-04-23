using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class DeliveryPayment : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

    }
}
