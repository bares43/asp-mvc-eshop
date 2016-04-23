using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Repository.Builder
{
    class Builder<T> where T : class, IEntity
    {
        protected DbSet<T> DbSet { get; }
        protected IQueryable<T> query;

        public IQueryable<T> Query => query;

        public Builder(DbSet<T> DbSet)
        {
            this.DbSet = DbSet;
            query = DbSet;
        }

        public Builder<T> AddPagging(int skip, int take)
        {
            query = Query.OrderBy(i => i.Id).Skip(skip).Take(take);
            return this;
        }

        public Builder<T> Id(int id)
        {
            query = Query.Where(i => i.Id == id);
            return this;
        }   
        
        public List<T> ToList()
        {
            return Query.ToList();
        }

        public T First()
        {
            return Query.FirstOrDefault();
        }

        public int Count()
        {
            return Query.Count();
        }
    }
}
