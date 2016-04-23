using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Address : IEntity
    {
        public enum AddressTypes { Delivery, Billing}

        public int Id { get; set; }

        public AddressTypes Type { get; set; }

        public bool Primary { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Created { get; set; }

        [DisplayName("Jméno")]
        public string Name { get; set; }

        [DisplayName("Příjmení")]
        public string Surname { get; set; }

        [DisplayName("Ulice")]
        public string Street { get; set; }

        [DisplayName("č.p.")]
        public string StreedCode { get; set; }

        [DisplayName("Město")]
        public string City { get; set; }

        [DisplayName("PSČ")]
        public string ZipCode { get; set; }

        public bool Equals(Address address)
        {
            return Name.Equals(address.Name) && Surname.Equals(address.Surname) && Street.Equals(address.Street) &&
                   StreedCode.Equals(address.StreedCode) && City.Equals(address.City) && ZipCode.Equals(address.ZipCode);
        }

    }
}
