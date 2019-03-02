namespace Linkedin.Layers.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;


    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class
        where TContext : DbContext
    {

        TContext context;
        DbSet<TEntity> entitySet;


        public Repository(TContext context)
        {
            this.context = context;
            this.entitySet = context.Set<TEntity>();
        }


        public TEntity Add(TEntity entity)
        {
            entitySet.Add(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }

        public DbSet<TEntity> GetAll()
        {
            return entitySet;
        }

        public List<TEntity> GetAllBind()
        {
            return entitySet.ToList();
        }

        public List<TEntity> GetAllBindInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            return entitySet.Include(path).ToList();
        }

        public TEntity GetById(params object[] id)
        {
            return entitySet.Find(id);
        }

        public bool Remove(TEntity entity)
        {
            entitySet.Remove(entity);
            return context.SaveChanges() > 0;
        }

        public bool Update(TEntity entity)
        {
            entitySet.AddOrUpdate(entity);
            return context.SaveChanges() > 0;
        }
    }
}