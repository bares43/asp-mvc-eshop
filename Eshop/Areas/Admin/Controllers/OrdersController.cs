using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using Glimpse.Core.Extensions;
using Eshop.Class;

namespace Eshop.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class OrdersController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();
        
        // GET: Admin/Orders
        public ActionResult Index(int page = 1)
        {
            var totalItems = unitOfWork.RepositoryOrder.Count();

            var paging = new Paginator() { CurrentPage = page, TotalItems = totalItems };


            var orders = unitOfWork.RepositoryOrder.GetAllWithItemsAndPaging(paging.Skip, paging.ItemsPerPage);

            ViewBag.Paging = paging;


            return View(orders);
        }

        public ActionResult Detail(int idorder)
        {
            var order = unitOfWork.RepositoryOrder.GetWithDetail(idorder);

            return View(order);
        }

        public ActionResult ChangeState(int idOrder, Order.ORDER_STATE orderState)
        {
            var order = unitOfWork.RepositoryOrder.Get(idOrder);
            var user = (Administrator)unitOfWork.RepositoryUser.GetUserByEmail(User.Identity.Name);

            order.OrderState = orderState;

            if (orderState == Order.ORDER_STATE.Completed)
            {
                order.Completed = DateTime.Now;
            }

            unitOfWork.RepositoryOrder.Update(order);

            var orderHistory = new OrderHistory()
            {
                Order = order,
                OrderState = orderState,
                Date = DateTime.Now,
                ChangedBy = user,
                Type = OrderHistory.OrderHistoryTypes.OrderStateChange,
                Message = $"Objednávka je nyní ve stavu {orderState.ToDescription()}"
            };

            unitOfWork.RepositoryOrderHistory.Add(orderHistory);
            unitOfWork.Save();

            var response = new JsonResponseHelper();
            response.AddNotySuccess($"Objednávka č. {order.Id} je nyní ve stavu '{orderState.ToDescription()}'");

            return Json(response.Sections.ToArray());
        }
    }
}