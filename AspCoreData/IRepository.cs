using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AspCoreData
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(long Id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
