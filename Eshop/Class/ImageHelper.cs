using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using DataAccess;
using DataAccess.Class;

namespace Eshop.Class
{
    public class ImageHelper
    {
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public static DataAccess.Model.Image CreateThumbnail(DataAccess.Model.Image image, int width, int height)
        {
            var img = Image.FromFile($"{DataAccess.Model.Image.Path}{image.Id}");


            var small = ScaleImage(img, width, height);

            var thumbnailModel = new DataAccess.Model.Image
            {
                Width = width,
                Height = height,
                RealWidth = small.Width,
                RealHeight = small.Height,
                ContentType = image.ContentType,
                ParentId = image.Id,
                GalleryId = image.GalleryId,
                Visible = true
            };

            var unitOfWork = new UnitOfWork();
            unitOfWork.RepositoryImage.Add(thumbnailModel);
            unitOfWork.Save();
            
            var smallThumbnail = new Bitmap(small);

            switch (image.ContentType)
            {
                case "image/jpeg":
                    smallThumbnail.Save($"{DataAccess.Model.Image.Path}{thumbnailModel.Id}", ImageFormat.Jpeg);
                    break;
                case "image/png":
                    smallThumbnail.Save($"{DataAccess.Model.Image.Path}{thumbnailModel.Id}", ImageFormat.Png);
                    break;
            }

            img.Dispose();
            smallThumbnail.Dispose();

            return thumbnailModel;
        }

    }
}