using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Model;
using Eshop.Class;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Eshop.Models.View
{
    public class OrderForm
    {
        public IList<Address> DeliveryAddresses { get; set; }
        public IList<Address> BillingAddresses { get; set; }
        public IList<Delivery> DeliveryWays { get; set; } 
        public IList<Payment> PaymentWays { get; set; } 
        public ICart Cart { get; set; }
        public bool IsAuthenticated { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Váš e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Vaše jméno")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Vaše příjmení")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím Váš telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Zvolte prosím druh platby")]
        public int Payment { get; set; }

        [Required(ErrorMessage = "Zvolte prosím druh dopravy")]
        public int Delivery { get; set; }

        public int? DeliveryAddressId { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací jméno")]
        public string DeliveryName { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací příjmení")]
        public string DeliverySurname { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací ulici")]
        public string DeliveryStreet { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací č.p.")]
        public string DeliveryStreetCode { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací město")]
        public string DeliveryCity { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím doručovací PSČ")]
        public string DeliveryZipCode { get; set; }

        public int? BillingAddressId { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební jméno")]
        public string BillingName { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební příjmení")]
        public string BillingSurname { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební ulici")]
        public string BillingStreet { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební č.p.")]
        public string BillingStreetCode { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební město")]
        public string BillingCity { get; set; }
        [Required(ErrorMessage = "Vyplňte prosím platební PSČ")]
        public string BillingZipCode { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}