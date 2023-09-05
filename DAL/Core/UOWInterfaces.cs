using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Core;
using System.Linq.Expressions;

namespace DAL.Core
{
    public interface IRepos
    {
        void Delete(int id, bool withSave = true);
        void Save();
    }


    public interface IRepos<TEntity> : IRepos where TEntity : class
    {


        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

        IQueryable<TEntity> GetAll(bool noTracking = true);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true);

        TEntity FirstOrDefault();

        TEntity Find(int id);

        void Create(TEntity item, bool withSave = true);

        void Update(TEntity item, bool withSave = true);

        void Save(TEntity item, bool withSave = true);

        void Remove(TEntity item);

        void Delete(IEnumerable<int> ids, bool withSave = true);


    }



    public interface IUnitOfWorkReadOnly : IDisposable
    {
        Database EFDatabase { get; }

        QContext DbContext { get; }

        IRepos<T> GetRepository<T>() where T : class, IEntity;
    }

    public interface IUnitOfWorkEx : IUnitOfWorkReadOnly
    {


        void Save();

        void Save<T>(T item, bool withSave = true) where T : class, IEntity;

        T Get<T>(int id) where T : class, IEntity;

        void Delete<T>(int id, bool withSave = true)
            where T : class, IEntity;
        void Remove<T>(T entity)
            where T : class, IEntity;

    }



}
