using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Class;
using DataAccess.Interfaces;

namespace DataAccess.Repository
{
    public class Repository<T> : IDisposable where T : class, IEntity
    {
        protected EshopContext context = null;
        private bool disposed = false;

        protected DbSet<T> DbSet
        {
            get; set;
        }
        
        public Repository(EshopContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }
        
        public IList<T> GetAll()
        {
            return DbSet.ToList();
        }
        
        public IList<P> GetAllByType<P>()
        {
            return DbSet.OfType<P>().ToList<P>();
        }
        
        public P GetByType<P>(int id) where P : class, IEntity
        {
            return DbSet.OfType<P>().FirstOrDefault<P>(i => i.Id == id);
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public int CountByType<E>() where E : IEntity
        {
            return DbSet.OfType<E>().Count();
        }

        public void AttachEntities<TE>(T entity, IList<TE> entities, Expression<Func<T,IList<TE>>> expression) where TE : IEntity
        {
            var getCollection = expression.Compile();
            var existingEntity = DbSet.Include(expression).FirstOrDefault<T>(p => p.Id == entity.Id);
            if (existingEntity != null)
            {
                var deletedEntities = getCollection(existingEntity).Except(entities).ToList<TE>();
                var addedEntities = entities.Except(getCollection(existingEntity)).ToList<TE>();
                deletedEntities.ForEach(c => getCollection(existingEntity).Remove(c));
                foreach (var c in addedEntities)
                {
                    getCollection(existingEntity).Add(c);
                }
            }

        }

        public List<T> GetAllWithPaging(int skip, int take)
        {
            return DbSet.OrderBy(p => p.Id).Skip(skip).Take(take).ToList();
        }

        public List<E> GetAllByTypeWithPaging<E>(int skip, int take) where E : class, IEntity
        {
            return DbSet.OfType<E>().OrderBy(p => p.Id).Skip(skip).Take(take).ToList();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }
}
