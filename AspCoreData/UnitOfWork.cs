using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspCoreData
{
    public class UnitOfWork<T> : IUnitOfWork where T : SchoolDataContext, new()
    {
        private readonly SchoolDataContext _context;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork()
        {
            _context = new T();
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this._disposed = true;
            }
        }
    }
}
