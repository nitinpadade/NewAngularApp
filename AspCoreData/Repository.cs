using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreData
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SchoolDataContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(SchoolDataContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> All()
        {
            return _dbset;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
    }
}
