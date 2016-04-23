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
    public class CategoriesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Categories
        public ActionResult Index(int page = 1)
        {

            var totalItems = unitOfWork.RepositoryCategory.Count();

            var paging = new Paginator() { CurrentPage = page, TotalItems = totalItems };

            var categories = unitOfWork.RepositoryCategory.GetAllWithPaging(paging.Skip, paging.ItemsPerPage);

            ViewBag.Paging = paging;

            return View(categories);
        }

        public ActionResult Add()
        {
            var category = new Category();

            return View("Edit", category);
        }

        public ActionResult Edit(int idcategory)
        {
            var category = unitOfWork.RepositoryCategory.Get(idcategory);

            return View(category);
        }

        public ActionResult Insert(Category category)
        {
            unitOfWork.RepositoryCategory.Add(category);
            unitOfWork.Save();

            return RedirectToAction("Index");

        }

        public ActionResult Update(Category category)
        {
            unitOfWork.RepositoryCategory.Update(category);
            unitOfWork.Save();

            return RedirectToAction("Index");

        }
    }
}