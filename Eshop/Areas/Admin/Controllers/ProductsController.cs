using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using Eshop.Areas.Admin.Models.View;
using Eshop.Class;
using Eshop.Models.View;

namespace Eshop.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(Role.ROLES.Administrator))]
    public class ProductsController : Controller
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Admin/Products
        public ActionResult Index(int page = 1)
        {
            var totalItems = unitOfWork.RepositoryProduct.Count();

            var paginator = new Paginator() { CurrentPage = page, TotalItems = totalItems };
            
            var products = unitOfWork.RepositoryProduct.GetAllWithPaging(paginator.Skip,paginator.ItemsPerPage);
            
            var productListView = new ProductsList() {Products = products, Paginator = paginator};

            return View(productListView);
        }

        public ActionResult Add()
        {
            var product = new Product {Categories = new List<Category>()};

            var categories = unitOfWork.RepositoryCategory.GetAll();

            ViewBag.Categories = categories;

            return View("Edit", product);
        }

        public ActionResult Edit(int idproduct)
        {
            var product = unitOfWork.RepositoryProduct.GetWithCategories(idproduct);

            var categories = unitOfWork.RepositoryCategory.GetAll();

            ViewBag.Categories = categories;

            return View(product);
        }

        public ActionResult Update(Product product, int[] category_id)
        {
            var categories = new List<Category>();

            if (category_id != null)
            {
                foreach (var idCategory in category_id)
                {
                    var category = unitOfWork.RepositoryCategory.Get(idCategory);
                    categories.Add(category);
                }
            }

            unitOfWork.RepositoryProduct.Update(product);
            unitOfWork.Save();
            unitOfWork.RepositoryProduct.AttachEntities(product, categories, p => p.Categories);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Insert(Product product, int[] category_id)
        {
            product.Categories = new List<Category>();

            foreach (var idCategory in category_id)
            {
                var category = unitOfWork.RepositoryCategory.Get(idCategory);
                category.Products = new List<Product>() {product};
                unitOfWork.RepositoryCategory.Update(category);
                product.Categories.Add(category);
            }

            
            unitOfWork.RepositoryProduct.Add(product);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public ActionResult ChangeVisibility(int idProduct, bool visible)
        {
            var product = unitOfWork.RepositoryProduct.Get(idProduct);

            product.Visible = visible;
            unitOfWork.RepositoryProduct.Update(product);
            
            unitOfWork.Save();

            var response = new JsonResponseHelper();
            response.AddNotySuccess($"Produkt {product.Name} (id: {product.Id}) je nyní {(product.Visible ? "viditelný" : "neviditelný") }");

            return Json(response.Sections.ToArray());
        }

        public ActionResult ChangeHighlight(int idProduct, bool highlight)
        {
            var product = unitOfWork.RepositoryProduct.Get(idProduct);

            product.Highlighted = highlight;
            unitOfWork.RepositoryProduct.Update(product);
            
            unitOfWork.Save();

            var response = new JsonResponseHelper();
            response.AddNotySuccess($"Produkt {product.Name} (id: {product.Id}) {(product.Highlighted ? "je" : "není") } zvýrazněn");

            return Json(response.Sections.ToArray());
        }
    }
}