using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using DataAccess.Repository;
using Eshop.Models.View;

namespace Eshop.Controllers
{
    public class PagesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Pages
        public ActionResult Contact()
        {
            var page = unitOfWork.RepositoryPage.GetByIdentificator("kontakty");

            return BasePage(page);
        }
        
        public ActionResult Terms()
        {
            var page = unitOfWork.RepositoryPage.GetByIdentificator("obchodni-podminky");

            return BasePage(page);
        }

        private ActionResult BasePage(Page page)
        {
            var pageView = new PageDetail() { Page = page };

            if (page.GalleryId != null)
            {
                var images = unitOfWork.RepositoryImage.GetParentImagesByGallery((int)page.GalleryId, true);
                pageView.Images = images;
            }

            if (page.FileStorageId != null)
            {
                var files = unitOfWork.RepositoryFile.GetByRepository(page.FileStorageId.Value, true);
                pageView.Files = files;
            }


            return View("BasePage", pageView);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}