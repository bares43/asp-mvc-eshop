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
    public class GalleriesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Galleries
        public ActionResult Index(int page = 1)
        {
            var totalItems = unitOfWork.RepositoryGallery.Count();

            var paging = new Paginator() { CurrentPage = page, TotalItems = totalItems };

            var galleries = unitOfWork.RepositoryGallery.GetAllWithPaging(paging.Skip, paging.ItemsPerPage);

            ViewBag.Paging = paging;

            return View(galleries);
        }

        public ActionResult Add(string AddToEntityType, int AddToEntityId = 0)
        {
            var gallery = new Gallery() {Visible = true};
            unitOfWork.RepositoryGallery.Add(gallery);

            if (AddToEntityType != null && AddToEntityId > 0)
            {
                switch (AddToEntityType)
                {
                    case nameof(Product):
                        var product = unitOfWork.RepositoryProduct.Get(AddToEntityId);
                        if (product != null)
                        {
                            product.Gallery = gallery;
                            unitOfWork.RepositoryProduct.Update(product);
                        }
                        break;
                    case nameof(Page):
                        var page = unitOfWork.RepositoryPage.Get(AddToEntityId);
                        if (page != null)
                        {
                            page.Gallery = gallery;
                            unitOfWork.RepositoryPage.Update(page);
                        }
                        break;
                }
            }

            unitOfWork.Save();
            return RedirectToAction("Edit", new {idGallery = gallery.Id});
        }

        public ActionResult Edit(int idgallery)
        {
            var gallery = unitOfWork.RepositoryGallery.Get(idgallery);

            var galleryEdit = new GalleryEdit() {Gallery = gallery};

            var images = unitOfWork.RepositoryImage.GetParentImagesByGallery(idgallery);

            galleryEdit.Images = images;

            return View(galleryEdit);
        }

        public ActionResult Upload(HttpPostedFileBase picture, int galleryId, string description)
        {

            var gallery = unitOfWork.RepositoryGallery.Get(galleryId);

            if (picture != null)
            {
                var resolutions = new[]
                {
                    new {Width = 75, Height = 56},
                    new {Width = 160, Height = 120},
                    new {Width = 270, Height = 203},
                    new {Width = 500, Height = 375},
                    new {Width = 800, Height = 600},
                }.ToList();


                if (picture.ContentType == "image/jpeg" || picture.ContentType == "image/png")
                {
                    var model = new Image() {Gallery = gallery, Name = picture.FileName, ContentType = picture.ContentType, Visible = true, Description = description};

                    var img = System.Drawing.Image.FromStream(picture.InputStream);
                    
                    var bitmap = new Bitmap(img);

                    model.Height = img.Height;
                    model.Width = img.Width;
                    model.RealWidth = img.Width;
                    model.RealHeight = img.Height;

                    unitOfWork.RepositoryImage.Add(model);
                    unitOfWork.Save();

                    switch (model.ContentType)
                    {
                        case "image/jpeg":
                            bitmap.Save($"{Image.Path}{model.Id}", ImageFormat.Jpeg);
                            break;
                        case "image/png":
                            bitmap.Save($"{Image.Path}{model.Id}", ImageFormat.Png);
                            break;
                    }


                    foreach (var resolution in resolutions)
                    {
                        ImageHelper.CreateThumbnail(model, resolution.Width, resolution.Height);
                    }


                    img.Dispose();
                    bitmap.Dispose();

                }
            }

            return RedirectToAction("Edit", new { idGallery = gallery.Id });
        }

        public ActionResult CreateThumbnail(int idimage, int width, int height)
        {
            var image = unitOfWork.RepositoryImage.Get(idimage);
            if (image != null)
            {
                ImageHelper.CreateThumbnail(image, width, height);
                return RedirectToAction("Edit", new { idGallery = image.GalleryId });
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteImage(int idimage)
        {
            var image = unitOfWork.RepositoryImage.GetImageWithThumbnailsAndGallery(idimage);

            if (image != null)
            {
                var idGallery = image.Gallery.Id;

                var ids = new List<int> {image.Id};
                ids.AddRange(image.Thumbnails.Select(thumbnail => thumbnail.Id));

                foreach (var id in ids)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/uploads/images/" + id)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/uploads/images/" + id));
                    }
                    unitOfWork.RepositoryImage.Delete(id);
                }

                unitOfWork.Save();

                return RedirectToAction("Edit", new {idGallery = idGallery});
            }
            else
            {
                return RedirectToAction("Index");
            }

           
        }

        public ActionResult ChangeVisibility(int idImage, bool visible)
        {
            var response = new JsonResponseHelper();

            var image = unitOfWork.RepositoryImage.Get(idImage);

            if (image != null)
            {
                var thumbnails = unitOfWork.RepositoryImage.GetThumbnailsFor(idImage);

                image.Visible = visible;
                unitOfWork.RepositoryImage.Update(image);

                if (thumbnails != null)
                {
                    foreach (var thumbnail in thumbnails)
                    {
                        thumbnail.Visible = visible;
                        unitOfWork.RepositoryImage.Update(thumbnail);
                    }
                }

                unitOfWork.Save();

                response.AddNotySuccess($"Obrázek {image.Name} (id: {image.Id}) je nyní {(image.Visible ? "viditelný" : "neviditelný") }");
            }
            
            return Json(response.Sections.ToArray());
        }
    }
}