using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData.Contract
{
    public interface IQueryWithParameters<TResult, TParameters>
    {
        TResult Execute(TParameters parameters);
    }
}
