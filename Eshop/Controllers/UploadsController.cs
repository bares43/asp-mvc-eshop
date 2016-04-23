using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;

namespace Eshop.Controllers
{
    public class UploadsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public FileResult GetImage(int idImage, int? width = null, int? height = null)
        {
            var image = unitOfWork.RepositoryImage.GetWithParent(idImage);

            if (image != null && image.Visible)
            {
                var name = image.Name;
                if (width.HasValue && height.HasValue && (image.Width != width.Value || image.Height != height.Value))
                {
                    image = unitOfWork.RepositoryImage.GetThumbnail(image.Parent?.Id ?? image.Id, width.Value, height.Value);
                    if (image == null) return null;
                }

                string filename = name;
                string filepath = $"{Image.Path}{image.Id}";
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                string contentType = image.ContentType;

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = filename,
                    Inline = true,
                };

                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(filedata, contentType);
            }
            else
            {
                return null;
            }
        }

        public FileResult GetFile(int idFile)
        {
            var image = unitOfWork.RepositoryFile.Get(idFile);

            if (image != null && (image.Visible || User.IsInRole(Role.ROLES.Administrator.ToString())))
            {

                string filename = image.Name;
                string filepath = $"{DataAccess.Model.File.Path}{image.Id}";
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                string contentType = image.ContentType;

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = filename,
                    Inline = false,
                };

                Response.AppendHeader("Content-Disposition", cd.ToString());

                return File(filedata,  contentType);
            }
            else
            {
                return null;
            }
        }
    }
}