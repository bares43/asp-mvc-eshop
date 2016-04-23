using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess.Repository.Builder
{
    class BuilderProduct : Builder<Product>
    {
        public BuilderProduct(DbSet<Product> DbSet) : base(DbSet)
        {
        }

        public BuilderProduct IsVisible(bool isVisible = true)
        {
            query = Query.Where(p => (p.Visible && isVisible) || (!p.Visible && !isVisible));
            return this;
        }

        public BuilderProduct IsHighlighted(bool isHighlighted = true)
        {
            query = Query.Where(p => (p.Highlighted && isHighlighted) || (!p.Highlighted && !isHighlighted));
            return this;
        }

        public BuilderProduct SearchInNameOrDescriptionInFullDescription(string phrase)
        {
            query =
                Query.Where(
                    p => p.Name.Contains(phrase) || p.Description.Contains(phrase) || p.FullDescription.Contains(phrase));
            return this;
        }

        public BuilderProduct InCategory(Category category)
        {
            query = Query.Where(c => c.Categories.Any(a => a.Id == category.Id));
            return this;
        }

        public BuilderProduct IncludeCategories()
        {
            query = Query.Include(p => p.Categories);
            return this;
        }

        public BuilderProduct WhereIdProduct(int idProduct)
        {
            query = Query.Where(p => p.Id == idProduct);
            return this;
        }
        
    }
}
