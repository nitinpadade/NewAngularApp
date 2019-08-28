using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData
{
    public class QueryResultList<T>
    {
        public List<T> Data { get; set; }
        public Dictionary<string, List<T>> DataList { get; set; }
        public object SubData { get; set; }
        public int TotalCount { get; set; }

        public double TotalPages { get; set; }

        public bool IsExecuted { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public CommandQueryStatus Status { get; set; }

    }
}
