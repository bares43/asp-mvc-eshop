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
    public class MenuController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Menu
        public ActionResult MainMenu()
        {
            var items = new List<MenuItem>
            {
                new MenuItem() {Action = "Index", Controller = "Home", Caption = "Dashboard", Icon = "dashboard"},
                new MenuItem() {Action = "Index", Controller = "Orders", Caption = "Objednávky", Icon = "shopping-cart"},
                new MenuItem() {Action = "Index", Controller = "Products", Caption = "Produkty", Icon = "cubes"},
                new MenuItem() {Action = "Index", Controller = "Categories", Caption = "Kategorie", Icon = "folder-open"},
                new MenuItem() {Action = "Index", Controller = "Pages", Caption = "Stránky", Icon = "newspaper-o"},
                new MenuItem() {Action = "Index", Controller = "Galleries", Caption = "Galerie", Icon = "image"},
                new MenuItem() {Action = "Index", Controller = "Files", Caption = "Soubory", Icon = "file"},
                new MenuItem() {Action = "Index", Controller = "Customers", Caption = "Zákazníci", Icon = "child"},
                new MenuItem() {Action = "Index", Controller = "Users", Caption = "Administrátorské účty", Icon = "users"},
                new MenuItem() {Action = "Index", Controller = "Settings", Caption = "Nastavení", Icon = "gear"},
                new MenuItem() {Action = "Logout", Controller = "Login", Caption = "Odhlásit", Icon = "arrow-circle-o-right"},
            };

            var user = unitOfWork.RepositoryUser.GetUserByEmail(User.Identity.Name);

            ViewBag.UserName = $"{user.Name} {user.Surname}";

            return View(items);
        }
    }
}