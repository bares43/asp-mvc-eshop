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
    public class UsersController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Users
        public ActionResult Index(int page = 1)
        {
            var paginator = new Paginator() {CurrentPage = page, TotalItems = unitOfWork.RepositoryUser.CountByType<Administrator>()};

            var users = unitOfWork.RepositoryUser.GetAllByTypeWithPaging<Administrator>(paginator.Skip, paginator.ItemsPerPage);

            ViewBag.Paginator = paginator;

            return View(users);
        }

        public ActionResult Add()
        {
            var user = new User();
            

            return View("Edit", user);
        }

        public ActionResult Edit(int idUser)
        {
            var user = unitOfWork.RepositoryUser.Get(idUser);

            return View(user);
        }

        public ActionResult Update(Administrator user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = unitOfWork.RepositoryUser.GetByType<Administrator>(user.Id);
                dbUser.Name = user.Name;
                dbUser.Surname = user.Surname;
                dbUser.Email = user.Email;

                unitOfWork.RepositoryUser.Update(dbUser);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", user);
            }
        }

        public ActionResult Insert(Administrator user)
        {
            if (ModelState.IsValid)
            {
                user.Created = DateTime.Now;
                unitOfWork.RepositoryUser.Add(user);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", user);
            }
        }

        public ActionResult ChangePasswordForm(int iduser)
        {
            var user = unitOfWork.RepositoryUser.GetByType<Administrator>(iduser);

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