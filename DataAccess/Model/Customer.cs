using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Customer : User
    {
        [DisplayName("Telefon")]
        public string Phone { get; set; }
    }
}
