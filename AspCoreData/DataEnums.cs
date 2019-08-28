using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreData
{
    class DataEnums
    {
    }

    public enum CommandQueryStatus
    {
        Default = 0,
        Executed,
        Failed,
        Warning,
        AccessDenied
    }
}
