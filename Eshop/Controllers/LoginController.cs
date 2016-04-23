using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess;
using DataAccess.Class;

namespace Eshop.Controllers
{
    public class LoginController : Controller
    {
        
        UnitOfWork unitOfWork = new UnitOfWork();
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string email, string password, string redirectTo)
        {
            if (Membership.ValidateUser(email, password))
            {
                var user = unitOfWork.RepositoryUser.GetUserByEmail(email);
                user.LastLogin = DateTime.Now;
                user.TotalLogins++;
                unitOfWork.RepositoryUser.Update(user);
                unitOfWork.Save();

                FormsAuthentication.SetAuthCookie(email, false);
                if (!string.IsNullOrEmpty(redirectTo))
                {
                    return Redirect(redirectTo);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["error"] = "Přihlášení se nepodařilo";

            return RedirectToAction("Index", "Login");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }

    }
}