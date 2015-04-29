using System;
using System.Collections.Generic;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Data
{
    public interface IGenericRepository<TEntity, in TKey> where TEntity : class
    {
        //        IQueryable<TEntity> AsQueryable();

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        //        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        //        TEntity First(Expression<Func<TEntity, bool>> predicate);
        //        TEntity GetById(TKey id);

        //        void Create(TEntity entity);
        //        void Update(TEntity entity);
        //void Delete(TKey id); //TEntity entity);
    }
}
