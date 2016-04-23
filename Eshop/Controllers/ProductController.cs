using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Repository;
using DataAccess.Model;
using Microsoft.SqlServer.Server;
using Eshop.Class;
using Eshop.Models.View;

namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Product
        public ActionResult Detail(int idProduct)
        {
            var product = unitOfWork.RepositoryProduct.Get(idProduct);

            if (product == null || !product.Visible) return View("NonExistent");

            var productDetailView = new ProductDetail() {Product = product};

            if (product.GalleryId != null)
            {
                var images = unitOfWork.RepositoryImage.GetParentImagesByGallery((int)product.GalleryId, true);
                productDetailView.Images = images;
            }

            if (product.FileStorageId != null)
            {
                var files = unitOfWork.RepositoryFile.GetByRepository(product.FileStorageId.Value, true);
                productDetailView.Files = files;
            }

            var relatedProducts = unitOfWork.RepositoryProduct.GetVisible();

            productDetailView.RelatedProducts = relatedProducts;

            return View(productDetailView);
        }

        public ActionResult List(int? idCategory = null, string phrase = null, int page = 1, int? limit = null)
        {
            var productListView = new ProductList();

            var paging = new Paginator() { CurrentPage = page};
            if (!string.IsNullOrEmpty(phrase))
            {
                paging.TotalItems = unitOfWork.RepositoryProduct.CountBySearch(phrase);
                paging.UrlParams["phrase"] = phrase;

                productListView.Products = unitOfWork.RepositoryProduct.Search(phrase, paging.ItemsPerPage, paging.Skip);
                productListView.Phrase = phrase;
            }
            else if (idCategory.HasValue)
            {
                var category = unitOfWork.RepositoryCategory.Get(idCategory.Value);
                if (category != null)
                {
                    productListView.Category = category;
                    paging.TotalItems = unitOfWork.RepositoryProduct.CountByCategory(category);
                    paging.UrlParams["idCategory"] = idCategory.ToString();

                    productListView.Products = unitOfWork.RepositoryProduct.GetByCategory(category, paging.ItemsPerPage, paging.Skip);
                }
            }

            productListView.Paginator = paging;

            return View(productListView);
        }

        public ActionResult Preview(int idProduct)
        {
            var product = unitOfWork.RepositoryProduct.Get(idProduct);

            var productPreviewView = new ProductPreview() {Product = product};

            if (product.GalleryId != null)
            {
                var image = unitOfWork.RepositoryImage.GetMainImageByThumbnail((int)product.GalleryId, 160, 120);
                productPreviewView.Image = image;
            }
            
            return View(productPreviewView);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}