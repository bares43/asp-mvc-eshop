using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;

namespace Eshop.Controllers
{
    public class MenuController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Menu
        public ActionResult CategoriesMenu()
        {

            var categories = unitOfWork.RepositoryCategory.GetMainCategories();

            return PartialView(categories);
        }
    }
}