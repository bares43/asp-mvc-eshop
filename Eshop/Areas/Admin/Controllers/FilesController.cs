using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using Eshop.Areas.Admin.Models.View;
using Eshop.Class;
using Image = DataAccess.Model.Image;

namespace Eshop.Areas.Admin.Controllers
{

    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class FilesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Galleries
        public ActionResult Index(int page = 1)
        {
            var totalItems = unitOfWork.RepositoryFileStorage.Count();

            var paging = new Paginator() { CurrentPage = page, TotalItems = totalItems };

            var storages = unitOfWork.RepositoryFileStorage.GetAllWithPaging(paging.Skip, paging.ItemsPerPage);

            ViewBag.Paging = paging;

            return View(storages);
        }

        public ActionResult Add(string addToEntityType = null, int addToEntityId = 0)
        {
            var storage = new FileStorage() {};
            unitOfWork.RepositoryFileStorage.Add(storage);

            if (addToEntityType != null && addToEntityId > 0)
            {
                switch (addToEntityType)
                {
                    case nameof(Product):
                        var product = unitOfWork.RepositoryProduct.Get(addToEntityId);
                        if (product != null)
                        {
                            product.FileStorage = storage;
                            unitOfWork.RepositoryProduct.Update(product);
                        }
                        break;
                    case nameof(Page):
                        var page = unitOfWork.RepositoryPage.Get(addToEntityId);
                        if (page != null)
                        {
                            page.FileStorage = storage;
                            unitOfWork.RepositoryPage.Update(page);
                        }
                        break;
                }
            }

            unitOfWork.Save();
            return RedirectToAction("Edit", new {idFileStorage = storage.Id});
        }

        public ActionResult Edit(int idFileStorage)
        {
            var storage = unitOfWork.RepositoryFileStorage.Get(idFileStorage);

            var fileStorageEdit = new FileStorageEdit() {FileStorage = storage};

            var files = unitOfWork.RepositoryFile.GetByRepository(idFileStorage);

            fileStorageEdit.Files = files;

            return View(fileStorageEdit);
        }

        public ActionResult Upload(HttpPostedFileBase file, int fileStorageId, string description)
        {

            var storage = unitOfWork.RepositoryFileStorage.Get(fileStorageId);

            if (file != null)
            {

                var model = new File()
                {
                    FileStorage = storage,
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    Visible = true,
                    Description = description
                };
                    
                unitOfWork.RepositoryFile.Add(model);
                unitOfWork.Save();

                file.SaveAs($"{DataAccess.Model.File.Path}{model.Id}");
             
            }

            return RedirectToAction("Edit", new { idFileStorage = storage.Id });
        }
        
        public ActionResult DeleteFile(int idFile)
        {
            var file = unitOfWork.RepositoryFile.Get(idFile);

            if (file != null)
            {
                var idFileStorage = file.FileStorageId;

                if (System.IO.File.Exists($"{DataAccess.Model.File.Path}{file.Id}"))
                {
                    System.IO.File.Delete($"{DataAccess.Model.File.Path}{file.Id}");
                }

                unitOfWork.RepositoryImage.Delete(file.Id);
                unitOfWork.Save();

                return RedirectToAction("Edit", new {idFileStorage = idFileStorage});
            }
            else
            {
                return RedirectToAction("Index");
            }

            
        }

        public ActionResult ChangeVisibility(int idFile, bool visible)
        {
            var file = unitOfWork.RepositoryFile.Get(idFile);

            file.Visible = visible;
            unitOfWork.RepositoryFile.Update(file);

            unitOfWork.Save();

            var response = new JsonResponseHelper();
            response.AddNotySuccess($"Soubor {file.Name} (id: {file.Id}) je nyní {(file.Visible ? "viditelný" : "neviditelný") }");

            return Json(response.Sections.ToArray());
        }
    }
}