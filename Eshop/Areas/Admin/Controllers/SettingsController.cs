using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using Eshop.Class;

namespace Eshop.Areas.Admin.Controllers
{

    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class SettingsController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            var delivery = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Delivery>();
            var payment = unitOfWork.RepositoryDeliveryPayment.GetAllByType<Payment>();

            ViewBag.Delivery = delivery;
            ViewBag.Payment = payment;

            return View();
        }

        public JsonResult AddDeliveryPayment(string type, string name, int price)
        {
            DeliveryPayment way;

            if (type == nameof(Delivery))
            {
                way = new Delivery();
            }
            else
            {
                way = new Payment();
            }

            way.Price = price;
            way.Name = name;

            unitOfWork.RepositoryDeliveryPayment.Add(way);
            unitOfWork.Save();

            var result = new JsonResponseHelper();
            result.AddNotySuccess($"Metoda {name} byla vytvořena");
            result.AddJs($"addDeliveryPayment('{type.ToLower()}','{name}','{price:C0}');");
            result.AddJs($"clearForm('{type.ToLower()}');");

            return Json(result.Sections.ToArray());
        }
    }
}