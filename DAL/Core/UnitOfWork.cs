using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace DAL.Core
{

    public class UnitOfWork : IUnitOfWorkEx
    {
        private bool _disposed;
        private readonly Dictionary<string, object> _repos = new Dictionary<string, object>();

        public IQContext DbContext { get; }
        public object EFDatabase => DbContext.Database;

        Database IUnitOfWorkReadOnly.EFDatabase => throw new NotImplementedException();

        public UnitOfWork()
        {
            DbContext = new QContext();
        }

        public UnitOfWork(IQContext context)
        {
            DbContext = context;
        }
        public void Save()
        {

//			DbContext.Database.EnsureDeleted();
//			DbContext.Database.EnsureCreated();
			DbContext.SaveChanges();
        }

        public void Save<T>(T item, bool withSave = true) where T : class, IEntity
        {
            GetRepository<T>().Save(item, withSave);
        }

        public T Get<T>(int id) where T : class, IEntity
        {

            return GetRepository<T>().GetAll().FirstOrDefault(x => x.Id == id);

        }

        public void Delete<T>(int id, bool withSave = true)
            where T : class, IEntity
        {
            GetRepository<T>().Delete(id, withSave);
        }

        public void Remove<T>(T entity)
            where T : class, IEntity
        {
            GetRepository<T>().Remove(entity);
        }
        public IRepos<T> GetRepository<T>() where T : class, IEntity
        {
            IRepos<T> rep;
            if (_repos.ContainsKey(typeof(T).Name))
            {
                rep = (IRepos<T>)_repos[typeof(T).Name];
            }
            else
            {
                rep = new Repository<T>(DbContext, RemembeNewEntity);
                _repos.Add(typeof(T).Name, rep);
            }

            return rep;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    (DbContext as DbContext).Dispose();
                }
                _disposed = true;
            }
        }

        #region for test

        public static bool RemembeNewEntity;

        static internal void AddCreatedEntity(IEntity entity, IRepos repository)
        {
            if (RemembeNewEntity)
            {
                addedForTest.Add((entity, repository));
            }
        }
        static List<(IEntity entity, IRepos repository)> addedForTest = new List<(IEntity entity, IRepos repository)>();

        public static void DeleteTestAdded()
        {
            foreach (var added in addedForTest)
            {
                try
                {
                      added.repository.Delete((int)added.entity.Id);
                }
                catch
                {
                    //todo:add a check to exclude already deleted ones
                }
            }
        }


        public static List<T> GetAdded<T>() where T : class, IEntity
        {
            List<T> added = new List<T>();
            foreach (var rep in addedForTest)
            {
                if (rep.entity.GetType() == typeof(T))
                    added.Add(rep.entity as T);
            }
            return added;
        }


        #endregion

    }

    public class Repository<T> : IRepos<T> where T : class, IEntity
    {

        bool RemembeNewEntity;

        IQContext _db;

        public Repository(IQContext db, bool remembeNewEntity)
        {
            _db = db;
            RemembeNewEntity = remembeNewEntity;
        }

        public void Create(T item, bool withSave = true)
        {

            _db.Entry(item).State = EntityState.Added;
       //     _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
if (withSave) Save();
#if DEBUG

            if (RemembeNewEntity)
                UnitOfWork.AddCreatedEntity(item, this);
#endif

        }

        public void Save(T item, bool withSave = true)
        {
            if (item.Id == 0 || item.Id == null)
            {
                Create(item, withSave);
            }
            else
            {
                Update(item, withSave);
            }
        }

        public void Save()
        {


            _db.SaveChanges();
        }
        public void Delete(int id, bool withSave = true)
        {
            var entity = Find(id);
            _db.Set<T>().Remove(entity);
            if (withSave) Save();

        }

        public void Delete(T entity, bool withSave = true)
        {
            Delete((int)entity.Id, withSave);

        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<int> ids, bool withSave = true)
        {
            var items = _db.Set<T>().Where(s => ids.Contains((int)s.Id)).ToList();
            _db.Set<T>().RemoveRange(items);
            if (withSave) Save();
        }

        public T Find(int id)
        {
            return _db.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll(bool noTracking = true)
        {
            if (noTracking) return _db.Set<T>().AsNoTracking();

            return _db.Set<T>();
        }


        public void Update(T item, bool withSave = true)
        {
            _db.Entry(item).State = EntityState.Modified;
            if (withSave) Save();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            return GetAll(noTracking).Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            return GetAll(noTracking).FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool noTracking = true)
        {
            return GetAll(noTracking).Where(predicate);
        }

        public T FirstOrDefault()
        {
            return GetAll(false)?.FirstOrDefault();
        }
    }

}