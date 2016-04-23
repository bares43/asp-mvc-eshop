using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model 
{
    public class Order : IEntity
    {
        public enum ORDER_STATE
        {
            [Display(Name = "Nová")]
            [Description("Nová")]
            Created,
            [Display(Name = "Zpracovává se")]
            [Description("Zpracovává se")]
            Proccesing,
            [Display(Name = "Čeká na platbu")]
            [Description("Čeká na platbu")]
            WaitingForPayment,
            [Display(Name = "Vyřízená")]
            [Description("Vyřízená")]
            Completed,
            [Display(Name="Stornovaná")]
            [Description("Stornovaná")]
            Storno
        }
        
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Completed { get; set; }

        public ORDER_STATE OrderState { get; set; }

        public virtual List<OrderHistory> OrderHistories { get; set; } 

        public virtual Address DeliveryAddress { get; set; }

        public virtual Address BillingAddress { get; set; }

        public virtual Delivery Delivery { get; set; }

        public int DeliveryPrice { get; set; }
        
        public virtual Payment Payment { get; set; }

        public int PaymentPrice { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        [NotMapped]
        public double TotalPrice
        {
            get { return OrderItems.Sum(p => p.Amount*p.Price) + PaymentPrice + DeliveryPrice; }
        }

        
    }
}
