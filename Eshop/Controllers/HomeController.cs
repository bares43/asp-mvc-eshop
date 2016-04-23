using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Repository;
using DataAccess.Model;
using Eshop.Class;
using Eshop.Models.View;

namespace Eshop.Controllers
{
    public class HomeController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            var products = unitOfWork.RepositoryProduct.GetHighlighted();

            ViewBag.HighlightedProducts = products;

            return View();
        }

        public ActionResult Header()
        {
            User user = null;

            if (User.IsInRole(Role.ROLES.Customer.ToString()))
            {
                user = unitOfWork.RepositoryUser.GetUserByEmail(User.Identity.Name);
            }

            return PartialView(user);
        }

        public ActionResult Paging(string action, string controller, Paginator paginator)
        {
            var paging = new Paging() { Paginator = paginator, Action = action, Controller = controller };

            return View(paging);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}