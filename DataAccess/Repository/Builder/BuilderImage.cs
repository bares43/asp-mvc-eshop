using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Migrations;
using DataAccess.Model;

namespace DataAccess.Repository.Builder
{
    class BuilderImage : Builder<Image>
    {
        public BuilderImage(DbSet<Image> DbSet) : base(DbSet)
        {
        }

        public BuilderImage IsVisible(bool isVisible = true)
        {
            query = Query.Where(p => (p.Visible && isVisible) || (!p.Visible && !isVisible));
            return this;
        }

        public BuilderImage InGallery(int idGallery)
        {
            query = Query.Where(i => i.Gallery.Id == idGallery);
            return this;
        }

        public BuilderImage IsParent()
        {
            query = Query.Where(i => i.Parent == null);
            return this;
        }

        public BuilderImage HasParent(int idImage)
        {
            query = Query.Where(i => i.Parent.Id == idImage);
            return this;
        }

        public BuilderImage HasResolution(int width, int height)
        {
            query = Query.Where(i => i.Width == width && i.Height == height);
            return this;
        }

        public BuilderImage IncludeThumbnails()
        {
            query = Query.Include(i => i.Thumbnails);
            return this;
        }

        public BuilderImage IncludeGallery()
        {
            query = Query.Include(i => i.Gallery);
            return this;
        }

        public BuilderImage IncludeParent()
        {
            query = Query.Include(i => i.Parent);
            return this;
        }
    }
}
