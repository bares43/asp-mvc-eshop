using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Role : IEntity
    {
        public enum ROLES {Customer, Administrator};

        public int Id { get; set; }

        public string Identificator { get; set; }

        public string Description { get; set; }

        public virtual List<User> Users { get; set; } 
    }
}
