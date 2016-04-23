using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Class
{
    public interface ICart
    {
        List<CartItem> Items { get; set; }

        double GetTotalPrice();

        int GetAmount();

        int GetAmountUnique();
    }
}
