using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Commit();
    }
}
