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
    public class CustomersController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Clients
        public ActionResult Index(int page = 1)
        {

            var paginator = new Paginator() {CurrentPage = page, TotalItems = unitOfWork.RepositoryUser.CountByType<Customer>()};

            var users = unitOfWork.RepositoryUser.GetAllByTypeWithPaging<Customer>(paginator.Skip, paginator.ItemsPerPage);

            ViewBag.Paginator = paginator;

            return View(users);
        }

        public ActionResult Add()
        {
            var client = new Customer();
            
            return View("Edit", client);
        }

        public ActionResult Edit(int idUser)
        {
            var user = unitOfWork.RepositoryUser.Get(idUser);

            return View(user);
        }

        public ActionResult Update(Customer user)
        {
            unitOfWork.RepositoryUser.Update(user);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Insert(Customer user)
        {
            user.Created = DateTime.Now;
            unitOfWork.RepositoryUser.Add(user);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public ActionResult ChangePasswordForm(int iduser)
        {
            var user = unitOfWork.RepositoryUser.GetByType<Customer>(iduser);

            return View(user);
        }

        public ActionResult ChangePassword(Administrator user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = unitOfWork.RepositoryUser.Get(user.Id);
                dbUser.ClearPassword = user.ClearPassword;
                dbUser.ConfirmPassword = user.ConfirmPassword;
                unitOfWork.RepositoryUser.Update(dbUser);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View("ChangePasswordForm", user);
            }
        }
    }
}