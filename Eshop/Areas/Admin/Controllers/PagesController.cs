using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;

namespace Eshop.Areas.Admin.Controllers
{

    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class PagesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Pages
        public ActionResult Index()
        {
            var pages = unitOfWork.RepositoryPage.GetAll();

            return View(pages);
        }

        public ActionResult Add()
        {
            return View("Edit", new Page());
        }

        public ActionResult Edit(int idPage)
        {
            var page = unitOfWork.RepositoryPage.Get(idPage);
            return View(page);
        }

        public ActionResult Update(Page page)
        {
            unitOfWork.RepositoryPage.Update(page);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Insert(Page page)
        {
            unitOfWork.RepositoryPage.Add(page);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}