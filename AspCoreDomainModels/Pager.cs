using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreDomainModels
{
    public class Pager
    {
        public string OrderBy { get; set; }

        public bool Direction { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

    }
}
