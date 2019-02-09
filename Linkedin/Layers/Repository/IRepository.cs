using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Linkedin.Layers.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Remove(TEntity entity);

        bool Update(TEntity entity);

        TEntity Add(TEntity entity);

        TEntity GetById(params object[] id);

        List<TEntity> GetAllBind();

        DbSet<TEntity> GetAll();
    }
}