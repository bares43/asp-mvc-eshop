using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Interfaces;
using DataAccess.Model;
using DataAccess.Repository.Builder;
using Glimpse.Core.Extensions;

namespace DataAccess.Repository
{
    public class RepositoryProduct : Repository<Product>
    {
        public RepositoryProduct(EshopContext context) : base(context)
        {
        }

        public List<Product> GetVisible()
        {
            return new BuilderProduct(DbSet).IsVisible().ToList();
        } 

        public List<Product> GetHighlighted(int? take = null, int? skip = null)
        {
            var builder = new BuilderProduct(DbSet).IsVisible().IsHighlighted();

            if (take != null && skip != null)
            {
                builder.AddPagging(skip.Value, take.Value);
            }

            return builder.ToList();
        }

        public List<Product> Search(string phrase, int? take = null, int? skip = null)
        {
            var builder = new BuilderProduct(DbSet).IsVisible().SearchInNameOrDescriptionInFullDescription(phrase);

            if (take != null && skip != null)
            {
                builder.AddPagging(skip.Value, take.Value);
            }

            return builder.ToList();
        }

        public int CountBySearch(string phrase)
        {
            var builder = new BuilderProduct(DbSet).IsVisible().SearchInNameOrDescriptionInFullDescription(phrase);

            return builder.Count();
        }

        public List<Product> GetByCategory(Category category, int? take = null, int? skip = null)
        {
            var builder = new BuilderProduct(DbSet).IsVisible().InCategory(category);

            if (take != null && skip != null)
            {
                builder.AddPagging(skip.Value, take.Value);
            }

            return builder.ToList();
        }

        public int CountByCategory(Category category)
        {
            var builder = new BuilderProduct(DbSet).IsVisible().InCategory(category);
            
            return builder.Count();
        }

        public Product GetWithCategories(int idProduct)
        {
            var builder = new BuilderProduct(DbSet).IncludeCategories().WhereIdProduct(idProduct);

            return builder.Query.FirstOrDefault();
        }

        public int GetSoldOutCount()
        {
            return
                DbSet.Count(p => p.Visible && p.StockQuantity == 0 && p.StockStatus != Product.STOCK_STATUS.Unavailable);
        }

        public int GetProductsInStockCount()
        {
            var sum =
                DbSet.Where(p => p.Visible && p.StockStatus != Product.STOCK_STATUS.Unavailable);

            if(sum.Any())
                    return sum.Sum(p => p.StockQuantity);

            return 0;
        }
       

      /*  public void SetMN<T>(string coll)
        {
            DbSet.Include(coll);
        }*/

       
    }
}
