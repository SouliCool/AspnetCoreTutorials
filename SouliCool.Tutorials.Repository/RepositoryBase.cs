using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SouliCool.Tutorials.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected EntitiesDbContext EntitiesDbContext { get; set; }

        public RepositoryBase(EntitiesDbContext entitiesDbContext)
        {
            this.EntitiesDbContext = entitiesDbContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.EntitiesDbContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.EntitiesDbContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            this.EntitiesDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.EntitiesDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.EntitiesDbContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this.EntitiesDbContext.SaveChanges();
        }
    }
}
