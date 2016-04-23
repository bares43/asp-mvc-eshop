using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Model;
using DataAccess.Repository.Builder;

namespace DataAccess.Repository
{
    public class RepositoryImage : Repository<Image>
    {
        public RepositoryImage(EshopContext context) : base(context)
        {
        }

        public Image GetWithParent(int idImage)
        {
            return new BuilderImage(DbSet).IncludeParent().Id(idImage).First();
        }

        public List<Image> GetParentImagesByGallery(int idGallery, bool? visible = null)
        {
            var builder = new BuilderImage(DbSet);

            if (visible.HasValue)
            {
                builder.IsVisible(visible.Value);
            }

            builder.InGallery(idGallery);
            builder.IsParent();
            builder.IncludeThumbnails();

            return builder.ToList();
        }

        public Image GetImageWithThumbnailsAndGallery(int idImage)
        {
            var builder = new BuilderImage(DbSet);

            builder.Id(idImage);
            builder.IncludeGallery();
            builder.IncludeThumbnails();

            return builder.Query.FirstOrDefault();
        } 

        public List<Image> GetVisibleByGallery(int idGallery)
        {
            return new BuilderImage(DbSet).InGallery(idGallery).IncludeThumbnails().IsParent().ToList();
        }

        public Image GetMainImageByThumbnail(int idGallery, int width, int height)
        {
            return
                DbSet.FirstOrDefault(i => i.Width == width && i.Height == height && i.Gallery.Id == idGallery && i.Visible);
        }

        public List<Image> GetThumbnailsFor(int idImage)
        {
            return DbSet.Where(i => i.Parent.Id == idImage).ToList();
        }

        public Image GetThumbnail(int idImage, int width, int height)
        {
            return new BuilderImage(DbSet).HasParent(idImage).HasResolution(width, height).IsVisible().First();
        }
    }
}
