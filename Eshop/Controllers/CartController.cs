using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using Eshop.Class;
using System.Data.Entity.Infrastructure;

using System.Data.Entity;
using DataAccess.Class;
using Eshop.Models.View;

namespace Eshop.Controllers
{
    public class CartController : Controller
    {
        
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        // GET: SessionCart
        public ActionResult Index()
        {
            return View(new SessionCart());
        }

        public ActionResult Order()
        {
            var cart = new SessionCart();
            var isAuthenticated = User.Identity.IsAuthenticated && User.IsInRole(Role.ROLES.Customer.ToString());

            if (!cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            var viewOrderForm = new OrderForm();

            if (isAuthenticated)
            {
                var user = (Customer)unitOfWork.RepositoryUser.GetUserByEmail(User.Identity.Name);

                viewOrderForm.Email = user.Email;
                viewOrderForm.Name = user.Name;
                viewOrderForm.Surname = user.Surname;
                viewOrderForm.Phone = user.Phone;

                var deliveryAddresses = unitOfWork.RepositoryAddress.GetAddressesByUserAndType(user, Address.AddressTypes.Delivery);
                var billingAddresses = unitOfWork.RepositoryAddress.GetAddressesByUserAndType(user, Address.AddressTypes.Billing);

                viewOrderForm.DeliveryAddresses = deliveryAddresses;
                viewOrderForm.BillingAddresses = billingAddresses;

                var primaryDeliveryAddress = deliveryAddresses.FirstOrDefault(a => a.Primary);
                if (primaryDeliveryAddress != null)
                {
                    viewOrderForm.DeliveryName = primaryDeliveryAddress.Name;
                    viewOrderForm.DeliverySurname = primaryDeliveryAddress.Surname;
                    viewOrderForm.DeliveryStreet = primaryDeliveryAddress.Street;
                    viewOrderForm.DeliveryStreetCode = primaryDeliveryAddress.StreedCode;
                    viewOrderForm.DeliveryCity = primaryDeliveryAddress.City;
                    viewOrderForm.DeliveryZipCode = primaryDeliveryAddress.ZipCode;
                }

                var primaryBillingAddress = billingAddresses.FirstOrDefault(a => a.Primary);
                if (primaryBillingAddress != null)
                {
                    viewOrderForm.BillingName = primaryBillingAddress.Name;
                    viewOrderForm.BillingSurname = primaryBillingAddress.Surname;
                    viewOrderForm.BillingStreet = primaryBillingAddress.Street;
                    viewOrderForm.BillingStreetCode = primaryBillingAddress.StreedCode;
                    viewOrderForm.BillingCity = primaryBillingAddress.City;
                    viewOrderForm.BillingZipCode = primaryBillingAddress.ZipCode;
                }

            }


            var delivery = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Delivery>();
            var payment = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Payment>();

            viewOrderForm.DeliveryWays = delivery;
            viewOrderForm.PaymentWays = payment;

            viewOrderForm.IsAuthenticated = isAuthenticated;

            viewOrderForm.Cart = cart;

            return View(viewOrderForm);
        }

        [HttpPost]
        public ActionResult SendOrder(OrderForm orderForm)
        {

            Customer customer = null;
            var isAuthenticated = User.Identity.IsAuthenticated && User.IsInRole(Role.ROLES.Customer.ToString());
            var cart = new SessionCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            if (isAuthenticated)
            {
                customer = (Customer)unitOfWork.RepositoryUser.GetUserByEmail(User.Identity.Name);
            }
            else if (!string.IsNullOrEmpty(orderForm.Password))
            {
                customer = new Customer() {Email = orderForm.Email, Name = orderForm.Name, Surname = orderForm.Surname, Phone = orderForm.Phone, Created = DateTime.Now, ClearPassword = orderForm.Password, ConfirmPassword = orderForm.Password};
                unitOfWork.RepositoryUser.Add(customer);
            }

            if (!ModelState.IsValid)
            {
                var deliveryWays = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Delivery>();
                var paymentWays = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Payment>();

                orderForm.DeliveryWays = deliveryWays;
                orderForm.PaymentWays = paymentWays;

                if (isAuthenticated)
                {
                    var deliveryAddresses = unitOfWork.RepositoryAddress.GetAddressesByUserAndType(customer, Address.AddressTypes.Delivery);
                    var billingAddresses = unitOfWork.RepositoryAddress.GetAddressesByUserAndType(customer, Address.AddressTypes.Billing);

                    orderForm.DeliveryAddresses = deliveryAddresses;
                    orderForm.BillingAddresses = billingAddresses;
                }

                orderForm.Cart = cart;
                orderForm.IsAuthenticated = isAuthenticated;
                orderForm.Password = null;

                return View("Order", orderForm);
            }
            
            var deliveryAddress = new Address() { Name = orderForm.DeliveryName, Surname = orderForm.DeliverySurname, Street = orderForm.DeliveryStreet, StreedCode = orderForm.DeliveryStreetCode, City = orderForm.DeliveryCity, ZipCode = orderForm.DeliveryZipCode, Created = DateTime.Now, Type = Address.AddressTypes.Delivery};
            if (customer != null) deliveryAddress.User = customer;
            if (orderForm.DeliveryAddressId.HasValue && isAuthenticated)
            {
                var userDeliveryAddress = unitOfWork.RepositoryAddress.Get(orderForm.DeliveryAddressId.Value);
                if (customer != null && (userDeliveryAddress.UserId != customer.Id || userDeliveryAddress.Type != Address.AddressTypes.Delivery)) userDeliveryAddress = null;

                if (deliveryAddress.Equals(userDeliveryAddress)) deliveryAddress = userDeliveryAddress;
            }

            var billingAddress = new Address() { Name = orderForm.BillingName, Surname = orderForm.BillingSurname, Street = orderForm.BillingStreet, StreedCode = orderForm.BillingStreetCode, City = orderForm.BillingCity, ZipCode = orderForm.BillingZipCode, Created = DateTime.Now, Type = Address.AddressTypes.Billing};
            if (customer != null) billingAddress.User = customer;
            if (orderForm.BillingAddressId.HasValue && isAuthenticated)
            {
                var userBillingAddress = unitOfWork.RepositoryAddress.Get(orderForm.BillingAddressId.Value);
                if (customer != null && (userBillingAddress.UserId != customer.Id || billingAddress.Type != Address.AddressTypes.Billing)) userBillingAddress = null;

                if (billingAddress.Equals(userBillingAddress)) billingAddress = userBillingAddress;
            }

            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                items.Add(new OrderItem()
                {
                    ProductId = item.Product.Id,
                    Amount = item.Amount,
                    Price = item.Product.Price,
                    Name = item.Product.Name
                });

                item.Product.StockQuantity = Math.Max(0, item.Product.StockQuantity - item.Amount);
                unitOfWork.RepositoryProduct.Update(item.Product);
            }

            var delivery = (Delivery) unitOfWork.RepositoryDeliveryPayment.Get(orderForm.Delivery);
            var payment = (Payment) unitOfWork.RepositoryDeliveryPayment.Get(orderForm.Payment);

            var order = new Order()
            {
                BillingAddress = billingAddress,
                DeliveryAddress = deliveryAddress,
                OrderItems = items,
                Created = DateTime.Now,
                OrderState = DataAccess.Model.Order.ORDER_STATE.Created,
                Delivery = delivery,
                Payment = payment,
                DeliveryPrice = delivery.Price,
                PaymentPrice = payment.Price,
                Email = orderForm.Email,
                Name = orderForm.Name,
                Surname = orderForm.Surname,
                Phone = orderForm.Phone
            };

            if (customer != null)
            {
                order.Customer = customer;
            }
            
            unitOfWork.RepositoryOrder.Add(order);

            unitOfWork.RepositoryOrderHistory.Add(new OrderHistory()
            {
                Order = order,
                OrderState = DataAccess.Model.Order.ORDER_STATE.Created,
                Date = DateTime.Now,
                Message = "Objednávka byla přijata",
                Type = OrderHistory.OrderHistoryTypes.OrderStateChange
            });

            unitOfWork.Save();

            cart.Items.Clear();

            return View();
        }

        [HttpPost]
        public ActionResult Add(int idProduct, int amount)
        {
            if (amount > 0 && amount <= DataAccess.Model.OrderItem.MAX_AMOUNT_OF_PRODUCT)
            {
                var product = unitOfWork.RepositoryProduct.Get(idProduct);

                var cart = new SessionCart();
                cart.Items.Add(new CartItem {Amount = amount, Product = product});
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var cart = new SessionCart();
            cart.Items.RemoveAt(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Recount(int id, int amount)
        {
            var response = new JsonResponseHelper();

            var cart = new SessionCart();

            if (amount == 0)
            {
                response.AddNotySuccess($"Položka {cart.Items[id].Product.Name} byla z košíku odstraněn.");
                response.AddJs($"removeProductFromCart({id})");
                cart.Items.RemoveAt(id);
            }
            else if(amount > 0)
            {
                cart.Items[id].Amount = Math.Min(amount, OrderItem.MAX_AMOUNT_OF_PRODUCT);
            }

            response.AddJs($"cartSetAmount({id},{cart.Items[id].Amount})");
            response.AddJs("recountCart()");

            if (!cart.Items.Any())
            {
                response.AddRefresh();
            }

            return Json(response.Sections.ToArray());
        }
        
        public ActionResult Clear()
        {
            var cart = new SessionCart();
            cart.Items.Clear();

            return RedirectToAction("Index");
        }
        
        public ActionResult HeaderCart()
        {
            return PartialView(new SessionCart());
        }
        
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}