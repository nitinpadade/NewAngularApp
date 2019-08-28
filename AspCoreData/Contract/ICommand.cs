using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData.Contract
{
    public interface ICommand<T>
    {
        T Execute(T obj);
    }
}
