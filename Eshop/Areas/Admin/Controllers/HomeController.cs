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
using Eshop.Models.View;

namespace Eshop.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class HomeController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Home
        public ActionResult Index()
        {

            ViewBag.OrdersCreatedCount = unitOfWork.RepositoryOrder.GetCountByState(Order.ORDER_STATE.Created);
            ViewBag.OrdersWaitingForPaymentCount = unitOfWork.RepositoryOrder.GetCountByState(Order.ORDER_STATE.WaitingForPayment);
            ViewBag.ProductsSoldOutCount = unitOfWork.RepositoryProduct.GetSoldOutCount();
            ViewBag.ProductsInStockCount = unitOfWork.RepositoryProduct.GetProductsInStockCount();

            return View();
        }
        
        public ActionResult Paging(string action, string controller, Paginator paginator)
        {
            var paging = new Paging() { Paginator = paginator, Action = action, Controller = controller };

            return View(paging);
        }
    }
}