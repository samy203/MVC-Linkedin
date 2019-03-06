namespace Linkedin.Layers.Repository
{
    using Linkedin.Models;
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
            TEntity x;
            entitySet.Add(entity);
            x = context.SaveChanges() > 0 ? entity : null;
            return x;
        }

        public DbSet<TEntity> GetAll()
        {
            return entitySet;
        }

        public List<TEntity> GetAllBind()
        {
            var x = entitySet.ToList();
            return x;
        }

        public List<TEntity> GetAllBindInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            var x = entitySet.Include(path).ToList();
            return x;
        }

        public TEntity GetById(params object[] id)
        {
            var x = entitySet.Find(id);
            return x;
        }

        public bool Remove(TEntity entity)
        {
            entitySet.Remove(entity);
            var x = context.SaveChanges() > 0;
            return x;
        }

        public bool Update(TEntity entity)
        {
            entitySet.AddOrUpdate(entity);
            var x = context.SaveChanges() > 0;
            return x;
        }
    }
}